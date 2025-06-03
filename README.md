# 🍔 Sistema de Autoatendimento para Lanchonete

Este é um sistema backend de autoatendimento desenvolvido como parte do Tech Challenge da FIAP, utilizando **.NET Core**, **Arquitetura Hexagonal** e **Docker** para facilitar o deploy e execução local.

---

## 📦 Tecnologias Utilizadas

- .NET Core 9
- Entity Framework Core
- PostgreSQL (via Docker)
- Arquitetura Hexagonal
- Scalar (para documentação das APIs)
- Docker / Docker Compose

---

## 🚀 Como executar o projeto

### Pré-requisitos

- Docker
- Docker Compose

### Passos para rodar a aplicação

1. Clone o repositório:

```bash
git clone [https://github.com/seu-usuario/seu-repo.git](https://github.com/NaskIII/fiap-tech-challenge-1.git)
cd seu-repo
```

2. Suba a aplicação com Docker:

```bash
docker-compose up --build
```

> Isso irá iniciar tanto o backend (.NET) quanto o banco de dados SQL Server.

3. Acesse a documentação da API:

[https://localhost:8081/scalar](https://localhost:8081/scalar)

---

## 🔐 Acesso Admin

Um usuário admin padrão já está cadastrado na aplicação. Use as credenciais abaixo para realizar autenticação:

- **Email:** `admin@admin.com`
- **Senha:** `Admin123!`

> Após o login, você poderá cadastrar produtos, categorias, iniciar pedidos e mais.

---

## 📚 Funcionalidades Implementadas

- Cadastro e autenticação de cliente (via CPF)
- Gerenciamento de produtos e categorias
- Realização de pedidos com ou sem cliente identificado
- Processamento de pagamento simulado (Mercado Pago fake)
- Envio do pedido para a fila da cozinha
- Consulta de status do pedido

---

## 📁 Estrutura do Projeto

```
/src
  ├── Domain
  ├── Application
  ├── Infraestructure
  ├── Infraestructure.QueryExpressionBuilder
  └── Api
/docker-compose.yml
```

---

## ⚠️ Observações

- Os pagamentos são simulados (fake checkout).
- A fila da cozinha (`KitchenQueue`) é gerenciada via entidades de domínio, simulando o funcionamento de uma cozinha real.

---

## ✅ Entregáveis Atendidos

- Arquitetura hexagonal
- API REST com Swagger
- Cadastro e login de clientes
- CRUD de produtos e categorias
- Checkout com envio para fila da cozinha
- Documentação de DDD (fora deste repositório)

---

## 🧑‍💻 Autor

Projeto desenvolvido por [Raphael Nascimento] para a pós-graduação em Arquitetura de Software na FIAP.
