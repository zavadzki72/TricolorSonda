# Tricolor Sonda - Scraper

Este módulo é o responsável pela **coleta automatizada de dados** do projeto. Ele monitora o site **Transfermarkt** para obter informações atualizadas sobre transferências, rumores e estatísticas do São Paulo FC.

---

## 🚀 Tecnologias e Bibliotecas

O scraper foi desenvolvido em **Python 3** utilizando bibliotecas focadas em performance e simplicidade:

- **Requests:** Para requisições HTTP rápidas e eficientes.
- **BeautifulSoup4 (bs4):** Para parsing e extração de dados do HTML.
- **LXML:** Parser de alta performance utilizado pelo BeautifulSoup.
- **Logging:** Sistema nativo de logs para monitoramento da execução.
- **JSON/Hashlib:** Gerenciamento de cache e exportação de dados.

---

## ⚙️ Funcionalidades

### 1. Sistema de Cache Inteligente
Para evitar sobrecarga no servidor de origem e acelerar a execuções repetidas, o scraper implementa um sistema de cache local:
- As páginas baixadas são salvas em `.cache/`.
- O cache tem validade de **12 horas**.
- Se o arquivo existir e for recente, o download é ignorado.

### 2. Coleta de Dados
O script executa três etapas principais:

1. **Transferências Oficiais:**
   - Coleta chegadas e saídas da temporada atual (ID 2025).
   - Extrai valores, datas, tipos de negociação (empréstimo/definitivo).

2. **Rumores de Mercado:**
   - Varre o fórum de rumores do Transfermarkt.
   - Analisa as threads para identificar jogadores especulados.
   - Tenta determinar probabilidade e origem/destino.

3. **Perfil Detalhado de Jogadores:**
   - Para cada jogador identificado (transferência ou rumor), o scraper visita seu perfil individual.
   - Coleta dados como: Data de nascimento, Altura, Pé preferido, Vencimento do contrato.

---

## 🛠️ Como Executar

### Pré-requisitos
Certifique-se de ter o Python 3 instalado.

1. **Instale as dependências:**
   (Caso exista um arquivo `requirements.txt`, ou instale manualmente):
   ```bash
   pip install requests beautifulsoup4 lxml
   ```

2. **Execute o script:**
   ```bash
   python scraper.py
   ```

> **Nota:** A primeira execução pode demorar um pouco mais, pois o cache ainda estará vazio e o script fará o download de todas as páginas necessárias.

---

## 📂 Dados de Saída

Após a execução, o script gera dois arquivos principais no diretório:

### `dados_tricolor_v3.json`
Arquivo estruturado contendo todos os dados brutos. Ideal para ser consumido pelo Frontend ou API.
```json
{
  "entradas": [...],
  "saidas": [...],
  "rumores": [...],
  "jogadores": { "ID": { ... } }
}
```

### `relatorio_final.md`
Um relatório legível em Markdown, gerado automaticamente para conferência rápida dos dados coletados.

---

## 🛡️ Medidas de Segurança
O scraper utiliza **rotação de User-Agents** e **delays aleatórios** (2 a 4 segundos) entre requisições para simular navegação humana e evitar bloqueios.
