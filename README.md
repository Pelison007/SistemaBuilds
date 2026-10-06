# Sistema de Builds

Sistema desenvolvido em **C#** para gerenciamento de builds de personagens.

## Tecnologias utilizadas

* C#
* .NET
* ASP.NET Core
* Entity Framework Core
* SQLite
* Git
* GitHub
* Visual Studio Code

---

# Configuração inicial do projeto

## 1. Criar o repositório no GitHub

Criar um repositório no GitHub chamado:

```text
SistemaBuilds
```

Durante a criação do repositório:

* Adicionar um `README.md`
* Adicionar o `.gitignore` para C#

---

## 2. Clonar o repositório

No terminal, acessar a pasta onde deseja armazenar o projeto.

Exemplo:

```powershell
cd Desktop
```

Depois, clonar o repositório:

```powershell
git clone URL_DO_REPOSITORIO
```

O Git criará automaticamente a pasta `SistemaBuilds`.

Entrar na pasta:

```powershell
cd SistemaBuilds
```

> O `git clone` já configura o Git local e conecta o projeto ao repositório do GitHub. Por isso, não é necessário executar `git init`.

---

# Configuração da Solution

## 3. Criar a Solution

Dentro da pasta `SistemaBuilds`:

```powershell
dotnet new sln -n SistemaBuilds
```

Esse comando cria a Solution chamada `SistemaBuilds`.

---

## 4. Criar a API

Criar o projeto da API:

```powershell
dotnet new webapi -n SistemaBuilds.API
```

Isso cria a pasta:

```text
SistemaBuilds.API
```

contendo o projeto da API ASP.NET Core.

---

## 5. Adicionar a API à Solution

Adicionar o projeto da API à Solution:

```powershell
dotnet sln add .\SistemaBuilds.API\SistemaBuilds.API.csproj
```

Para verificar os projetos adicionados:

```powershell
dotnet sln list
```

Resultado esperado:

```text
Projetos
--------
SistemaBuilds.API\SistemaBuilds.API.csproj
```

---

## 6. Executar a API

Para executar o projeto:

```powershell
dotnet run
```

Esse comando inicia a API localmente.

---

# Banco de dados

## 7. Instalar o Entity Framework Core para SQLite

Instalar o pacote SQLite:

```powershell
dotnet add .\SistemaBuilds.API\SistemaBuilds.API.csproj package Microsoft.EntityFrameworkCore.Sqlite --version 10.0.12
```

Esse pacote permite utilizar o banco de dados SQLite através do Entity Framework Core.

---

## 8. Instalar o Entity Framework Core Design

Instalar o pacote Design:

```powershell
dotnet add .\SistemaBuilds.API\SistemaBuilds.API.csproj package Microsoft.EntityFrameworkCore.Design --version 10.0.12
```

Esse pacote fornece recursos necessários para trabalhar com o Entity Framework Core durante o desenvolvimento.

---

# Estrutura do banco de dados

## 9. Criar o AppDbContext

Foi criada a pasta:

```text
SistemaBuilds.API
└── Data
    └── AppDbContext.cs
```

O `AppDbContext` representa o contexto utilizado pelo Entity Framework Core para trabalhar com o banco de dados.

O banco utilizado pelo projeto será:

```text
SistemaBuilds.db
```

A conexão com o SQLite utiliza:

```text
Data Source=SistemaBuilds.db
```

---

# Entidades

## 10. Criar a pasta Models

Foi criada a pasta:

```text
SistemaBuilds.API
└── Models
```

Nessa pasta ficam as entidades que representam as tabelas do sistema.

Atualmente foram criadas:

```text
Models
├── Classe.cs
├── Personagem.cs
└── Equipamento.cs
```

### Classe

Representa as classes disponíveis para os personagens.

Possui:

* `Id`
* `Nome`

### Personagem

Representa os personagens cadastrados no sistema.

Possui:

* `Id`
* `Nome`
* `Nivel`
* `ClasseId`

### Equipamento

Representa os equipamentos utilizados nas builds.

Possui:

* `Id`
* `Nome`
* `Tipo`
* `Ataque`
* `Defesa`

Os relacionamentos entre essas entidades serão configurados nas próximas etapas.

---

# Git

Como o projeto foi criado a partir de um repositório clonado do GitHub, o Git já está configurado.

## 11. Verificar o estado do projeto

```powershell
git status
```

Esse comando mostra o estado atual dos arquivos do projeto.

---

## 12. Adicionar as alterações

```powershell
git add .
```

O `.` representa todos os arquivos novos ou modificados da pasta atual.

---

## 13. Criar um commit

```powershell
git commit -m "mensagem do commit"
```

O commit registra as alterações no histórico do projeto.

---

## 14. Enviar as alterações para o GitHub

```powershell
git push
```

Esse comando envia os commits locais para o repositório remoto no GitHub.

---

# Sincronização com o GitHub

Quando uma alteração for feita diretamente pelo site do GitHub, como uma alteração no `README.md`, é necessário atualizar o repositório local antes de continuar trabalhando.

No terminal:

```powershell
git pull origin main
```

Esse comando baixa as alterações do GitHub para o repositório local.

Depois de realizar alterações no projeto pelo VS Code:

```powershell
git add .
git commit -m "mensagem do commit"
git push
```

Dessa forma, as alterações feitas localmente são enviadas novamente para o GitHub.

---

# Estrutura atual do projeto

```text
SistemaBuilds
│
├── SistemaBuilds.sln
│
├── SistemaBuilds.API
│   ├── Data
│   │   └── AppDbContext.cs
│   │
│   ├── Models
│   │   ├── Classe.cs
│   │   ├── Personagem.cs
│   │   └── Equipamento.cs
│   │
│   ├── Program.cs
│   ├── SistemaBuilds.API.csproj
│   └── ...
│
├── .gitignore
└── README.md
```

---

# Desenvolvimento

O sistema será desenvolvido utilizando uma API em C#, banco de dados e entidades relacionadas.

As próximas etapas serão adicionadas neste README conforme o desenvolvimento do projeto avançar.

Próximas etapas:

* Configurar os relacionamentos entre as entidades
* Configurar as tabelas no `AppDbContext`
* Criar as migrations
* Criar o banco de dados
* Desenvolver os endpoints da API
* Implementar o CRUD
* Implementar a funcionalidade de criação e gerenciamento das builds
* Calcular os atributos das builds dos personagens
