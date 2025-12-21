# Tricolor Sonda

O **Tricolor Sonda** é um projeto que tem como objetivo central **demonstrar e organizar todas as atividades relacionadas a transferências de jogadores**, incluindo entradas, saídas e análises financeiras associadas ao clube.

A aplicação consolida dados obtidos automaticamente via scraping e também permite inserções manuais, oferecendo uma visão clara e estruturada do mercado de transferências.

---

## 🎯 Objetivo do Projeto

- Centralizar informações sobre **transferências de jogadores**
- Facilitar a visualização de **entradas, saídas e desejos de mercado**
- Apresentar dados financeiros e de vendas de forma organizada
- Automatizar a coleta de dados a partir de fontes externas

---

## 🧩 Estrutura da Aplicação

Atualmente, o projeto possui **uma única página principal**, composta pelos seguintes elementos:

### Componentes da Interface
- **Header**
- **Seção Hero**
- **Tabelas Principais**
- **Footer**

### Tabelas Principais
- **Transferências**  
  Lista de jogadores negociados (entradas e saídas)

- **Lista de Desejos**  
  Jogadores monitorados ou desejados pelo clube

- **Dashboard Financeiro e de Vendas**  
  Visão consolidada dos dados financeiros relacionados às transferências

---

## 🏗️ Arquitetura do Projeto

O projeto é dividido em três grandes partes:

### 1. Frontend
- **Framework:** Angular  
- Responsável pela interface do usuário e visualização dos dados

### 2. Scraper
- **Linguagem:** Python  
- Responsável por coletar dados automaticamente do site **Transfermarkt**

### 3. API
- **Tecnologia:** .NET  
- Gerencia:
  - Exposição dos dados para o frontend
  - Inserções manuais
  - Comunicação com o banco de dados

---

## 🗄️ Banco de Dados

- **Banco:** MongoDB  
- Utilizado para armazenar:
  - Dados coletados pelo scraper
  - Registros inseridos manualmente via API

---

## 🔄 Fluxo de Dados

1. O **scraper** coleta informações do Transfermarkt
2. Os dados são armazenados no **MongoDB**
3. A **API** fornece esses dados ao frontend
4. O **frontend** apresenta as informações de forma estruturada e visual

---

## 🚧 Status do Projeto

- Estrutura inicial definida
- Página inicial implementada
- Novas funcionalidades e melhorias em andamento

---

## 📌 Tecnologias Utilizadas

- Angular
- Python
- .NET
- MongoDB

---

## 📖 Observações

Este projeto está em desenvolvimento e pode sofrer mudanças estruturais ao longo do tempo.
