# Sistema de Builds

API desenvolvida em **C#** para gerenciamento de builds de personagens, utilizando ASP.NET Core, Entity Framework Core e SQLite.

## Tecnologias utilizadas

- C#
- .NET 10
- ASP.NET Core
- Entity Framework Core
- SQLite
- Git
- GitHub
- Visual Studio Code

---

# 1. Pré-requisitos

Antes de começar, instale as seguintes ferramentas:

- **.NET 10 SDK:** para criar, compilar e executar o projeto.
- **Git:** para controlar as versões do código.
- **Visual Studio Code:** editor utilizado no desenvolvimento.

Links oficiais:

- [.NET](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/downloads)
- [Visual Studio Code](https://code.visualstudio.com/)

Para testar as requisições HTTP pelo VS Code, também é recomendada a extensão REST Client.

---

# 2. Criar o repositório no GitHub

Crie um repositório no GitHub com o nome:

```text
SistemaBuilds
```

Durante a criação, é possível adicionar:

- `README.md`
- `.gitignore` para C#

O arquivo `.gitignore` evita que arquivos desnecessários, como arquivos temporários e artefatos de compilação, sejam enviados ao repositório.

Repositório do projeto: https://github.com/Pelison007/SistemaBuilds

---

# 3. Clonar o repositório

Abra o terminal e acesse a pasta onde deseja armazenar o projeto.

Exemplo no Windows:

```powershell
cd "$HOME\Desktop"
```

Clone o repositório:

```powershell
git clone https://github.com/Pelison007/SistemaBuilds.git
```

Entre na pasta criada:

```powershell
cd SistemaBuilds
```

O comando `git clone` baixa os arquivos do repositório e configura a conexão com o GitHub.

**Importante:** ao clonar um repositório existente, não é necessário executar `git init`.

---

# 4. Criar a Solution e a API (configuração do zero)

Esta seção documenta os comandos utilizados para criar a estrutura inicial. Se você já clonou o repositório com a Solution e a API existentes, não precisa executar esses comandos novamente.

## 4.1. Criar a Solution

Dentro da pasta principal do projeto:

```powershell
dotnet new sln -n SistemaBuilds
```

Esse comando cria a Solution `SistemaBuilds.sln`, que organiza os projetos da aplicação.

## 4.2. Criar o projeto da API

```powershell
dotnet new webapi -n SistemaBuilds.API
```

Esse comando cria o projeto ASP.NET Core Web API na pasta `SistemaBuilds.API`.

## 4.3. Adicionar a API à Solution

```powershell
dotnet sln add .\SistemaBuilds.API\SistemaBuilds.API.csproj
```

Para verificar os projetos adicionados:

```powershell
dotnet sln list
```

O resultado deve incluir:

```text
SistemaBuilds.API\SistemaBuilds.API.csproj
```

---

# 5. Configurar o banco de dados

O projeto utiliza SQLite para armazenar os dados e Entity Framework Core para mapear as classes C# para tabelas do banco.

## 5.1. Instalar o pacote SQLite

Execute na pasta principal do projeto:

```powershell
dotnet add .\SistemaBuilds.API\SistemaBuilds.API.csproj package Microsoft.EntityFrameworkCore.Sqlite --version 10.0.12
```

Esse pacote permite que o Entity Framework Core trabalhe com o banco SQLite.

## 5.2. Instalar o pacote Design

```powershell
dotnet add .\SistemaBuilds.API\SistemaBuilds.API.csproj package Microsoft.EntityFrameworkCore.Design --version 10.0.12
```

Esse pacote fornece recursos utilizados durante o desenvolvimento, incluindo operações relacionadas às migrations.

## 5.3. Instalar ou verificar a ferramenta de migrations

A ferramenta `dotnet-ef` é utilizada para executar comandos do Entity Framework Core pelo terminal.

Para verificar se ela já está instalada:

```powershell
dotnet ef --version
```

Versão utilizada no desenvolvimento: `10.0.12`.

Se a ferramenta ainda não estiver instalada globalmente, utilize:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.12
```

Não é necessário executar a instalação novamente caso a ferramenta já esteja disponível.

---

# 6. Estrutura do banco de dados

## AppDbContext

O arquivo `AppDbContext.cs` fica na pasta `Data` e representa o contexto do Entity Framework Core.

Ele define os conjuntos de entidades utilizados pelo sistema:

- `Classes`
- `Personagens`
- `Equipamentos`

A conexão utiliza SQLite, com o arquivo:

```text
SistemaBuilds.db
```

A configuração da conexão utiliza:

```text
Data Source=SistemaBuilds.db
```

## Criar e atualizar o banco com migrations

Os comandos a seguir devem ser executados dentro da pasta da API:

```powershell
cd .\SistemaBuilds.API
```

Para criar uma migration inicial:

```powershell
dotnet ef migrations add CriacaoInicial
```

Para aplicar as migrations ao banco:

```powershell
dotnet ef database update
```

Para consultar as informações do contexto:

```powershell
dotnet ef dbcontext info
```

Para listar as migrations:

```powershell
dotnet ef migrations list
```

A migration inicial `CriacaoInicial` foi criada e aplicada durante o desenvolvimento. Ela registra a estrutura inicial das tabelas e o relacionamento entre personagens e classes.

O Entity Framework Core utiliza a tabela `__EFMigrationsHistory` para registrar as migrations aplicadas.

Quando for necessário alterar a estrutura do banco, crie uma nova migration e aplique-a:

```powershell
dotnet ef migrations add NomeDaAlteracao
dotnet ef database update
```

Substitua `NomeDaAlteracao` por um nome que descreva a modificação.

---

# 7. Entidades do sistema

As entidades representam os dados que serão armazenados no banco.

## Classe

Representa uma classe de personagem.

Propriedades:

- `Id`: identificador da classe.
- `Nome`: nome da classe.

Exemplos de classes para o sistema: Arqueiro, Guerreiro e Mago.

## Personagem

Representa um personagem cadastrado.

Propriedades:

- `Id`: identificador do personagem.
- `Nome`: nome do personagem.
- `Nivel`: nível do personagem.
- `ClasseId`: identificador da classe associada.
- `Classe`: propriedade de navegação para acessar a classe relacionada.

## Equipamento

Representa um equipamento utilizado na composição de builds.

Propriedades:

- `Id`: identificador do equipamento.
- `Nome`: nome do equipamento.
- `Tipo`: tipo do equipamento.
- `Ataque`: atributo de ataque.
- `Defesa`: atributo de defesa.

---

# 8. Relacionamento entre Classe e Personagem

A entidade `Personagem` possui a propriedade `ClasseId`, que funciona como chave estrangeira e referencia `Classe.Id`.

O relacionamento foi configurado no `AppDbContext` por meio dos métodos `HasOne()`, `WithMany()` e `HasForeignKey()`.

| Propriedade | Função |
|---|---|
| `Personagem.ClasseId` | Armazena o identificador da classe associada. |
| `Personagem.Classe` | Permite acessar a classe relacionada por meio da propriedade de navegação. |
| `Classe.Id` | Identifica a classe referenciada. |

**Cardinalidade:** uma classe pode estar associada a vários personagens, enquanto cada personagem está associado a uma classe.

Esse relacionamento permite representar no banco qual classe pertence a cada personagem.

---

# 9. Executar a API

Para iniciar a API, entre na pasta do projeto:

```powershell
cd .\SistemaBuilds.API
```

Execute:

```powershell
dotnet run
```

O terminal informará o endereço local em que a API está sendo executada.

Mantenha o terminal aberto enquanto realiza os testes. Se a porta informada for diferente da utilizada no arquivo de requisições, atualize a variável `@baseUrl`.

---

# 10. Testar as requisições HTTP

Foi criado o arquivo `Testes/Requisicoes.http` para organizar as requisições utilizadas durante o desenvolvimento.

Os testes podem ser executados pelo Visual Studio Code com a extensão REST Client.

## Rotas da API

### Classes

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/` | Retorna uma mensagem de apresentação da API. |
| GET | `/classes/listar` | Lista as classes cadastradas. |
| POST | `/classes/cadastrar` | Cadastra uma classe. |
| PUT | `/classes/atualizar/{id}` | Altera o nome de uma classe existente. |
| DELETE | `/classes/excluir/{id}` | Exclui uma classe existente. |

A rota de listagem retorna `200 OK` com os dados quando existem classes cadastradas e `400 Bad Request` quando a lista está vazia, conforme a implementação atual.

As rotas de alteração e exclusão retornam `404 Not Found` quando o identificador informado não corresponde a uma classe existente.

## Exemplos de requisições

O exemplo abaixo considera que a API está disponível em `http://localhost:5011`. Caso a porta seja diferente, ajuste a variável.

```http
@baseUrl = http://localhost:5011

###

GET {{baseUrl}}/

###

GET {{baseUrl}}/classes/listar
Accept: application/json

###

POST {{baseUrl}}/classes/cadastrar
Content-Type: application/json

{
    "nome": "Guerreiro"
}

###

PUT {{baseUrl}}/classes/atualizar/1
Content-Type: application/json

{
    "nome": "Paladino"
}

###

DELETE {{baseUrl}}/classes/excluir/1
```

**Atenção:** os identificadores utilizados nos exemplos devem corresponder aos registros existentes no banco. Evite executar a requisição DELETE sem conferir qual registro será excluído.

---

# 11. Controle de versão com Git e GitHub

O Git é utilizado para registrar as alterações realizadas durante o desenvolvimento, enquanto o GitHub hospeda o repositório remoto.

## Verificar o estado do projeto

```powershell
git status
```

## Adicionar as alterações

```powershell
git add .
```

## Criar um commit

```powershell
git commit -m "Descrição da alteração"
```

Exemplo:

```powershell
git commit -m "Implementa CRUD de classes"
```

## Enviar as alterações ao GitHub

```powershell
git push
```

É recomendável criar commits pequenos e descritivos após concluir etapas importantes, como a criação dos modelos, a configuração do relacionamento e a implementação das rotas.

## Sincronizar alterações feitas no GitHub

Se houver alterações realizadas diretamente pelo site do GitHub, atualize o repositório local antes de enviar novas alterações:

```powershell
git pull origin main
```

Depois de realizar suas modificações no VS Code:

```powershell
git add .
git commit -m "Descrição da alteração"
git push
```

Isso ajuda a manter o repositório local sincronizado com o remoto. Se houver conflitos, resolva-os antes de continuar.

---

# 12. Estrutura do projeto

A estrutura principal do projeto é organizada da seguinte maneira:

```text
SistemaBuilds/
├── SistemaBuilds.sln
├── SistemaBuilds.API/
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── Modelo/
│   │   ├── Classe.cs
│   │   ├── Personagem.cs
│   │   └── Equipamento.cs
│   ├── Migrations/
│   ├── Testes/
│   │   └── Requisicoes.http
│   ├── Program.cs
│   ├── SistemaBuilds.API.csproj
│   └── SistemaBuilds.db
├── .gitignore
└── README.md
```

A estrutura acima representa a organização esperada dos arquivos principais. Arquivos adicionais podem existir conforme a configuração do projeto.

---

# 13. Estado atual e próximos passos

## Implementado

- Estrutura inicial da Solution e da API.
- Modelos `Classe`, `Personagem` e `Equipamento`.
- Configuração do Entity Framework Core com SQLite.
- Migration inicial e criação da estrutura do banco.
- Relacionamento entre `Personagem` e `Classe`.
- Rotas de cadastro, listagem, alteração e exclusão de classes.
- Arquivo de requisições HTTP para testes.
- Versionamento do projeto com Git e GitHub.

## Próximas etapas

- Criar rotas para cadastrar e consultar personagens associados a classes existentes.
- Utilizar o relacionamento para apresentar os personagens junto de suas classes.
- Desenvolver uma funcionalidade que utilize os dados cadastrados para montar ou consultar builds de personagens.
- Testar os fluxos completos e preparar a demonstração do projeto.

