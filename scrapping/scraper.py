#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Scraper do Transfermarkt - São Paulo FC (Tricolor Sonda) - v3
Coleta transferências detalhadas, rumores profundos e perfis de jogadores.
"""

import json
import time
import random
import hashlib
import logging
import re
from datetime import datetime, timedelta
from pathlib import Path
from typing import Dict, List, Optional, Any

import requests
from bs4 import BeautifulSoup

# ==================== CONFIG LOGGING ====================
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# ==================== CONFIG GERAIS ====================
BASE_URL = "https://www.transfermarkt.com.br"
CLUB_ID = "585"
CLUB_SLUG = "sao-paulo-fc"
MONTHS_HISTORY = 6

# URLs Base
URL_TRANSFERS = f"{BASE_URL}/{CLUB_SLUG}/transfers/verein/{CLUB_ID}/saison_id/2025/pos//detailpos/0/w_s//plus/1"
URL_RUMORS_BASE = f"{BASE_URL}/sala-de-rumores/detail/forum/154/gk_verein_id/{CLUB_ID}"

SCRIPT_DIR = Path(__file__).parent.absolute()
CACHE_DIR = SCRIPT_DIR / ".cache"
CACHE_DIR.mkdir(exist_ok=True)

USER_AGENTS = [
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36"
]

# ==================== UTILITÁRIOS ====================

def get_headers() -> Dict[str, str]:
    return {
        "User-Agent": random.choice(USER_AGENTS),
        "Accept": "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
        "Referer": BASE_URL,
        "Accept-Language": "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7"
    }

def random_delay(min_s=1.5, max_s=3.0):
    time.sleep(random.uniform(min_s, max_s))

def get_cache_key(url: str) -> str:
    return hashlib.md5(url.encode()).hexdigest()

def fetch_page(url: str, use_cache=True) -> Optional[str]:
    cache_path = CACHE_DIR / f"{get_cache_key(url)}.json"
    
    if use_cache and cache_path.exists():
        try:
            with open(cache_path, 'r', encoding='utf-8') as f:
                data = json.load(f)
            # Cache válido por 12h
            cached_dt = datetime.fromisoformat(data['timestamp'])
            if datetime.now() - cached_dt < timedelta(hours=12):
                return data['html']
        except: pass

    random_delay(2, 4)
    try:
        resp = requests.get(url, headers=get_headers(), timeout=30)
        resp.raise_for_status()
        
        with open(cache_path, 'w', encoding='utf-8') as f:
            json.dump({
                'url': url, 
                'timestamp': datetime.now().isoformat(),
                'html': resp.text
            }, f)
        
        logger.info(f"FETCH: {url}")
        return resp.text
    except Exception as e:
        logger.error(f"Erro ao baixar {url}: {e}")
        return None

def clean_text(text: str) -> str:
    if not text: return ""
    return re.sub(r'\s+', ' ', text).strip()

def parse_tm_date(date_str: str) -> Optional[str]:
    """Tenta normalizar datas do TM para DD/MM/YYYY"""
    if not date_str: return None
    date_str = clean_text(date_str)
    
    # Tratamento de 'ontem'/'hoje'
    now = datetime.now()
    if 'hoje' in date_str.lower():
        return now.strftime("%d/%m/%Y")
    if 'ontem' in date_str.lower():
        return (now - timedelta(days=1)).strftime("%d/%m/%Y")
        
    patterns = [
        r'(\d{2}\.\d{2}\.\d{4})', # 01.01.2025
        r'(\d{2}/\d{2}/\d{4})'    # 01/01/2025
    ]
    
    for pat in patterns:
        match = re.search(pat, date_str)
        if match:
            d = match.group(1).replace('.', '/')
            return d
    return None

def extract_id_from_url(url: str) -> str:
    # .../profil/spieler/12345
    match = re.search(r'spieler/(\d+)', url)
    return match.group(1) if match else ""

# ==================== PARSERS ====================

def parse_player_profile(url: str) -> Dict:
    """Extrai detalhes do perfil do jogador."""
    default = {
        "nome_completo": "", "nascimento": "", 
        "altura": "", "pe": "", 
        "contrato_ate": ""
    }
    
    if not url: return default
    
    html = fetch_page(url)
    if not html: return default
    
    soup = BeautifulSoup(html, 'lxml')
    info = default.copy()
    
    # Busca tabela de info (.info-table)
    # Geralmente label: info-table__content--regular
    # Valor: info-table__content--bold
    
    labels = soup.find_all('span', class_='info-table__content--regular')
    for lbl in labels:
        txt = clean_text(lbl.get_text())
        val_span = lbl.find_next_sibling('span', class_='info-table__content--bold')
        if not val_span: continue
        val = clean_text(val_span.get_text())
        
        if "Nome completo" in txt: info['nome_completo'] = val
        if "Nasc." in txt: 
            # "16/03/2004 (20)" -> "16/03/2004"
            info['nascimento'] = val.split('(')[0].strip()
        if "Altura" in txt: info['altura'] = val
        if "Pé" in txt: info['pe'] = val
        if "Contrato até" in txt: info['contrato_ate'] = val
        
    return info

def parse_transfers(html: str) -> tuple:
    """Extrai transferências (entradas/saídas)."""
    t_in, t_out = [], []
    soup = BeautifulSoup(html, 'lxml')
    tables = soup.find_all('div', class_='responsive-table')
    
    for idx, div in enumerate(tables):
        is_incoming = (idx == 0) # 0=Entradas, 1=Saídas
        tbody = div.find('tbody')
        if not tbody: continue
        
        for row in tbody.find_all('tr', class_=['odd', 'even']):
            try:
                cells = row.find_all('td', recursive=False)
                if len(cells) < 7: continue
                
                # [1] Jogador
                p_info = {"nome": "N/A", "foto": "", "url": "", "posicao": "N/A"}
                c1 = cells[1].find('table', class_='inline-table')
                if c1:
                    img = c1.find('img')
                    if img: p_info['foto'] = img.get('data-src') or img.get('src')
                    
                    lnk = c1.find('td', class_='hauptlink').find('a')
                    if lnk:
                        p_info['nome'] = lnk.get_text(strip=True)
                        p_info['url'] = BASE_URL + lnk.get('href', '')
                    
                    rows_i = c1.find_all('tr')
                    if len(rows_i) > 1: p_info['posicao'] = rows_i[-1].get_text(strip=True)

                if p_info['nome'] == "N/A": continue
                
                # Detalhes básicos
                age = clean_text(cells[2].get_text())
                mkt_val = clean_text(cells[3].get_text())
                
                nat = "N/A"
                if cells[4].find('img'): nat = cells[4].find('img').get('title', 'N/A')
                
                # [5] Clube Parceiro
                club_name = "N/A"
                club_id_url = ""
                c5 = cells[5].find('table', class_='inline-table')
                if c5:
                    clnk = c5.find('td', class_='hauptlink').find('a')
                    if clnk: 
                        club_name = clnk.get_text(strip=True)
                        club_id_url = BASE_URL + clnk.get('href', '')
                else:
                    if cells[5].find('a'): club_name = cells[5].find('a').get_text(strip=True)

                # [6] Taxa / Valor
                fee_raw = clean_text(cells[6].get_text())
                is_loan = "Empréstimo" in fee_raw or "empréstimo" in fee_raw.lower()
                
                # Tentar extrair data fim emprestimo do texto fee (ex: "Fim do empréstimo31/12/2025")
                # Ou às vezes está numa coluna separada não mapeada? No plus/1 geralmente está textual.
                loan_end_date = ""
                match_date = re.search(r'(\d{2}/\d{2}/\d{4})', fee_raw)
                if match_date:
                    loan_end_date = match_date.group(1)
                
                # Montar objeto
                entry = {
                    "id": extract_id_from_url(p_info['url']),
                    "nome": p_info['nome'],
                    "nacionalidade": nat,
                    "idade": age,
                    "posicao": p_info['posicao'],
                    "valor_mercado": mkt_val,
                    "valor": fee_raw, # Custo/Valor
                    "is_emprestimo": is_loan,
                    "data_fim_emprestimo": loan_end_date,
                    "url": p_info['url'],
                    "foto": p_info['foto']
                }

                if is_incoming:
                    entry['origem'] = club_name
                    entry['destino'] = "São Paulo FC"
                    t_in.append(entry)
                else:
                    entry['origem'] = "São Paulo FC"
                    entry['destino'] = club_name
                    t_out.append(entry)

            except Exception as e:
                logger.warning(f"Erro linha transf: {e}")

    return t_in, t_out

def fetch_rumors_deep(base_url: str, limit_months: int = 6) -> List[Dict]:
    """Busca rumores paginados."""
    rumors = []
    page = 1
    cutoff_date = datetime.now() - timedelta(days=limit_months*30)
    
    while True:
        url = f"{base_url}/page/{page}"
        logger.info(f"RUMORES: Processando página {page}...")
        
        html = fetch_page(url)
        if not html: break
        
        soup = BeautifulSoup(html, 'lxml')
        # Rows: div.row.geruecht-kasten
        rows = []
        for d in soup.find_all('div'):
            cls = d.get('class', [])
            if 'row' in cls and 'geruecht-kasten' in cls:
                rows.append(d)
        
        if not rows: break # Fim das páginas
        
        found_old = False
        
        for row in rows:
            try:
                # Jogador
                p_name = "N/A"
                p_url = ""
                ndiv = row.find('div', class_='spielername')
                if ndiv and ndiv.find('a'):
                    p_name = clean_text(ndiv.get_text())
                    p_url = BASE_URL + ndiv.find('a').get('href', '')
                
                if p_name == "N/A": continue
                
                # Fonte / Tópico
                topic = "Rumor"
                sdiv = row.find('div', class_='wechsel-verein-name')
                if sdiv: topic = clean_text(sdiv.get_text())
                
                # Origem/Destino (via imagens na gk-wappenbox)
                # Geralmente, imagens com setinha
                origem, destino = "N/A", "N/A"
                
                # Parsing from Topic Text logic
                # "Transferência para o [XYZ]?"
                match_dest = re.search(r'para o (.*?)\?', topic, re.IGNORECASE)
                if match_dest:
                    destino = match_dest.group(1).strip()
                    # Se destino não é SPFC, origem é SPFC (rumor de saída)
                    # Se destino é SPFC, origem é Clube Atual
                    
                # Clube Atual (no box do jogador)
                curr_club = "N/A"
                cdiv = row.find('div', class_='vereinname')
                if cdiv: curr_club = clean_text(cdiv.get_text())
                
                if "São Paulo" in destino or "SPFC" in destino:
                    origem = curr_club
                else:
                    if not destino or destino == "N/A":
                        # Tentar inferir
                        pass
                    if "São Paulo" in curr_club:
                        origem = "São Paulo FC"

                # Datas e Criador ("Criado" e "Resposta")
                # Estrutura difícil via DIVs, tentar achar texto de data
                # Geralmente no fim da threadtext-zelle
                info_text = row.get_text(" ", strip=True)
                
                # Tentar extrair data dd.mm.yyyy
                dates = re.findall(r'\d{2}\.\d{2}\.\d{4}', info_text)
                last_date_str = dates[-1] if dates else None
                
                created_by = "?"
                # Tentar achar 'de [User]'
                
                # Check date limit
                if last_date_str:
                    dt = datetime.strptime(last_date_str, "%d.%m.%Y")
                    if dt < cutoff_date:
                        found_old = True
                
                probs = "?"
                # Tentar achar %
                match_pct = re.search(r'(\d{1,2})\s?%', info_text)
                if match_pct: probs = match_pct.group(1) + "%"

                rumors.append({
                    "id": extract_id_from_url(p_url),
                    "nome": p_name,
                    "origem": origem,
                    "destino": destino,
                    "avaliacao": probs, # Probabilidade
                    "status": "Rumor",
                    "criado_em": last_date_str, # Usando última atualização como proxy mais relevante
                    "criado_por": created_by, # Difícil extrair sem seletor preciso
                    "url": p_url
                })
                
            except Exception: pass
            
        page += 1
        if found_old or page > 5: # Safety break
            break
            
    return rumors

# ==================== MAIN ====================

def main():
    logger.info("=== TRICOLOR SONDA SCRAPER v3 ===")
    
    # 1. Transferências
    logger.info("Etapa 1: Transferências...")
    html_t = fetch_page(URL_TRANSFERS)
    t_in, t_out = [], []
    if html_t:
        t_in, t_out = parse_transfers(html_t)
        
    # 2. Rumores
    logger.info("Etapa 2: Rumores...")
    rumors = fetch_rumors_deep(URL_RUMORS_BASE, limit_months=MONTHS_HISTORY)
    
    # 3. Perfis de Jogadores (Unificar IDs únicos)
    logger.info("Etapa 3: Detalhes dos Jogadores...")
    all_player_urls = set()
    for x in t_in + t_out + rumors:
        if x.get('url'): all_player_urls.add(x['url'])
        
    players_db = {}
    total = len(all_player_urls)
    for idx, purl in enumerate(all_player_urls):
        pid = extract_id_from_url(purl)
        if not pid: continue
        
        logger.info(f"[{idx+1}/{total}] Perfil: {pid}")
        bio = parse_player_profile(purl)
        bio['id'] = pid
        players_db[pid] = bio
        
    # 4. Consolidar
    full_data = {
        "metadata": {
            "atualizado": datetime.now().isoformat(),
            "origem": "Transfermarkt"
        },
        "entradas": t_in,
        "saidas": t_out,
        "rumores": rumors,
        "jogadores": players_db
    }
    
    # Export JSON
    f_json = SCRIPT_DIR / "dados_tricolor_v3.json"
    with open(f_json, 'w', encoding='utf-8') as f:
        json.dump(full_data, f, indent=2, ensure_ascii=False)
        
    logger.info(f"Finalizado! Arquivo salvo: {f_json}")
    
    # Markdown Report
    f_md = SCRIPT_DIR / "relatorio_final.md"
    with open(f_md, 'w', encoding='utf-8') as f:
        f.write(f"# Dados Tricolor Sonda\nAtualizado: {full_data['metadata']['atualizado']}\n\n")
        
        f.write(f"## Entradas ({len(t_in)})\n")
        for x in t_in: f.write(f"- {x['nome']} ({x['origem']} -> {x['destino']}) | {x['valor']}\n")
        
        f.write(f"\n## Saídas ({len(t_out)})\n")
        for x in t_out: f.write(f"- {x['nome']} ({x['origem']} -> {x['destino']}) | {x['valor']}\n")
        
        f.write(f"\n## Rumores ({len(rumors)})\n")
        for x in rumors: 
            p = players_db.get(x['id'], {})
            f.write(f"- {x['nome']} ({x.get('origem')}?) [{x['avaliacao']}] - {p.get('posicao', '')}\n")

if __name__ == "__main__":
    main()
