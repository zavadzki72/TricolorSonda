# Tricolor Sonda - Frontend

Bem-vindo à documentação do frontend do **Tricolor Sonda**. Este projeto é a interface visual responsável por apresentar os dados de transferências, análises financeiras e listas de desejos do clube.

---

## 🚀 Tecnologias

O projeto é construído utilizando as seguintes tecnologias principais:

- **Angular** (v21.0.4)
- **TypeScript** (~5.9.2)
- **SCSS** (Sass)
- **Angular Material**
- **Angular SSR** (Server-Side Rendering)

---

## 📂 Estrutura do Projeto

A estrutura de diretórios foi organizada para garantir escalabilidade e manutenção:

```
src/
├── app/
│   ├── components/      # Componentes reutilizáveis de UI
│   │   ├── header/      # Cabeçalho da aplicação
│   │   ├── footer/      # Rodapé da aplicação
│   │   └── player-list/ # Listagem e cards de jogadores
│   ├── pages/           # Páginas principais (rotas)
│   │   └── home/        # Página inicial
│   ├── services/        # Serviços para comunicação com API e lógica de negócios
│   ├── styles/          # Arquivos de estilo globais (variáveis, mixins)
│   ├── app.config.ts    # Configuração da aplicação (Providers, Router)
│   └── app.routes.ts    # Definição das rotas
├── assets/              # Imagens, fontes e arquivos estáticos
└── styles.scss          # Estilos globais e resets
```

---

## 🎨 Design System

O projeto utiliza um sistema de design consistente focado na identidade visual tricolor.

### Tipografia

- **Fonte Principal:** `Google Sans Flex`
- **Características:** Sans-serif, moderna, com variações de peso e largura.

### Paleta de Cores

As cores são gerenciadas através de variáveis SCSS (`src/styles/_variables.scss`).

| Nome Variável      | Hexadecimal | Exemplo Visual |
|--------------------|-------------|----------------|
| `$light_red`       | `#d71920`   | 🔴 Vermelho Claro |
| `$red`             | `#b11116`   | 🔴 Vermelho Padrão |
| `$dark_red`        | `#8b0304`   | 🔴 Vermelho Escuro |
| `$brown`           | `#490000`   | 🟤 Marrom/Vinho |
| `$black`           | `#000000`   | ⚫ Preto |
| `$light_black`     | `#1a1d1f`   | ⚫ Preto Suave |
| `$light_black_2`   | `#212527`   | ⚫ Preto Suave 2 |
| `$dark_gray`       | `#48535a`   | 🔘 Cinza Escuro |
| `$gray`            | `#8a9297`   | 🔘 Cinza |
| `$white`           | `#ffffff`   | ⚪ Branco |
| `$dark_blue`       | `#002659`   | 🔵 Azul Escuro |
| `$blue`            | `#005496`   | 🔵 Azul Padrão |
| `$yellow`          | `#ffdd00`   | 🟡 Amarelo (Destaque) |

### Padrões de Estilo

- **Reset Global:** Aplicado em `styles.scss` para garantir consistência entre navegadores.
- **Componentização:** Estilos específicos de componentes devem permanecer em seus respectivos arquivos `.scss` (scoping).
- **Variáveis:** Sempre utilize as variáveis definidas em `_variables.scss` para cores e espaçamentos.

---

## 🛠️ Como Executar

### Pré-requisitos
- Node.js (versão compatível com Angular 21)
- NPM

### Passos

1. **Instale as dependências:**
   ```bash
   npm install
   ```

2. **Execute o servidor de desenvolvimento:**
   ```bash
   ng serve
   ```

3. **Acesse a aplicação:**
   Abra o navegador em `http://localhost:4200/`.

---

## 📦 Build e SSR

Para gerar os artefatos de produção:

```bash
ng build
```

Para testar a versão com Server-Side Rendering localmente:

```bash
npm run serve:ssr:tricolor-sonda-front
```
