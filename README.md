# Sistema de Builds

Sistema desenvolvido em **C#** para gerenciamento de builds de personagens.

## Tecnologias utilizadas

* C#
* .NET
* ASP.NET Core
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

# Git

Como o projeto foi criado a partir de um repositório clonado do GitHub, o Git já está configurado.

## 7. Verificar o estado do projeto

```powershell
git status
```

Esse comando mostra o estado atual dos arquivos do projeto.

---

## 8. Adicionar as alterações

```powershell
git add .
```

O `.` representa todos os arquivos novos ou modificados da pasta atual.

---

## 9. Criar um commit

```powershell
git commit -m "Montando estrutura inicial do projeto"
```

O commit registra as alterações no histórico do projeto.

---

## 10. Enviar as alterações para o GitHub

```powershell
git push
```

Esse comando envia os commits locais para o repositório remoto no GitHub.

---

# Estrutura inicial do projeto

```text
SistemaBuilds
│
├── SistemaBuilds.sln
│
├── SistemaBuilds.API
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
