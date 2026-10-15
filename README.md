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

# 10.1. CRUD de Personagem

Nesta etapa, foi desenvolvido o CRUD da entidade `Personagem`, permitindo cadastrar, listar, alterar e excluir personagens no banco de dados SQLite.

Também foi utilizado o relacionamento entre `Personagem` e `Classe`, garantindo que um personagem seja associado a uma classe existente.

## Rotas implementadas

| Método | Endpoint | Descrição |
|---|---|---|
| POST | `/personagens/cadastro` | Cadastra um novo personagem. |
| GET | `/personagens/listar` | Lista os personagens cadastrados. |
| PUT | `/personagens/alterar/{id}` | Atualiza um personagem existente. |
| DELETE | `/personagens/excluir/{id}` | Exclui um personagem pelo ID. |

## POST — Cadastrar personagem

A rota POST recebe os dados de um personagem e verifica se a classe informada existe antes de realizar o cadastro.

```csharp
app.MapPost("/personagens/cadastro", (
    [FromBody] Personagem personagem,
    [FromServices] AppDbContext ctx) =>
{
    var classe = ctx.Classes.Find(personagem.ClasseId);

    if (classe == null)
    {
        return Results.BadRequest("Essa classe não existe");
    }

    ctx.Personagens.Add(personagem);
    ctx.SaveChanges();

    return Results.Ok("Personagem criado com sucesso, Bom jogo!!");
});
```

### Explicação

- `[FromBody]`: recebe os dados enviados no corpo da requisição.
- `[FromServices]`: recebe o contexto do banco por injeção de dependência.
- `Find()`: procura uma entidade pela chave primária.
- `classe == null`: verifica se a classe não foi encontrada.
- `Add()`: adiciona o personagem ao contexto.
- `SaveChanges()`: grava o personagem no banco de dados.

O `Find()` retorna a entidade encontrada ou `null` caso não exista.

### Exemplo de requisição

```http
POST {{baseUrl}}/personagens/cadastro
Content-Type: application/json

{
    "nome": "Pelison",
    "nivel": 30,
    "classeId": 2
}
```

O `classeId` deve corresponder ao ID de uma classe existente.

---

## GET — Listar personagens

A rota GET consulta os personagens cadastrados.

Durante o desenvolvimento, foi estudado como utilizar `Include()` e `Select()` para trabalhar com os dados da classe relacionada.

### Utilizando Include()

```csharp
var personagens = ctx.Personagens
    .Include(p => p.Classe)
    .ToList();
```

O método `Include()` carrega a classe relacionada a cada personagem.

Dessa forma, ao retornar os objetos diretamente, a API pode apresentar:

```json
[
    {
        "id": 1,
        "nome": "Pelison",
        "nivel": 30,
        "classeId": 2,
        "classe": {
            "id": 2,
            "nome": "Mago"
        }
    }
]
```

Para utilizar `Include()`, é necessário importar:

```csharp
using Microsoft.EntityFrameworkCore;
```

### Utilizando Select()

O `Select()` permite escolher quais informações queremos obter na consulta.

```csharp
var personagens = ctx.Personagens
    .Select(p => new
    {
        p.Id,
        p.Nome,
        p.Nivel,
        Classe = p.Classe!.Nome
    })
    .ToList();
```

Nesse exemplo, selecionamos o ID, o nome, o nível e o nome da classe relacionada.

A expressão:

```csharp
Classe = p.Classe!.Nome
```

Significa:

- `p`: representa cada personagem da consulta.
- `p.Classe`: acessa a propriedade de navegação da classe relacionada.
- `.Nome`: acessa o nome dessa classe.
- `Classe =`: define o nome da propriedade que será criada no resultado.
- `!`: informa ao compilador que esperamos que `Classe` não seja nula, sem realizar uma verificação em tempo de execução.

O resultado fica mais simples:

```json
[
    {
        "id": 1,
        "nome": "Pelison",
        "nivel": 30,
        "classe": "Mago"
    }
]
```

### Diferença entre Include() e Select()

| Método | Função |
|---|---|
| `Include()` | Carrega entidades relacionadas. |
| `Select()` | Seleciona ou projeta as informações desejadas. |
| `Find()` | Procura uma entidade pela chave primária. |
| `Any()` | Verifica se existe algum registro que satisfaça uma condição. |
| `ToList()` | Executa a consulta e transforma os resultados em uma lista. |

**Aprendizado importante:** quando utilizamos `Select()` para acessar diretamente `p.Classe.Nome`, o Entity Framework Core consegue gerar a consulta necessária para buscar o nome da classe relacionada.

Por isso, nesse caso, não precisamos utilizar `Include()` junto com `Select()`.

### Verificar se existem personagens

Também utilizamos `Any()` para verificar se existe algum personagem cadastrado:

```csharp
if (!ctx.Personagens.Any())
{
    return Results.BadRequest("Não há personagens cadastrados");
}
```

O método `Any()` retorna `true` quando encontra pelo menos um registro e `false` quando não encontra nenhum.

O operador `!` inverte esse resultado.

Assim, `!ctx.Personagens.Any()` significa que não existem personagens cadastrados.

### Exemplo de requisição

```http
GET {{baseUrl}}/personagens/listar
Accept: application/json
```

---

## PUT — Alterar personagem

A rota PUT permite atualizar o nome, o nível e a classe de um personagem existente.

Antes de atualizar, verificamos se o personagem existe e se a nova classe informada está cadastrada.

```csharp
app.MapPut("/personagens/alterar/{id}", (
    int id,
    [FromBody] Personagem personagemAtualizado,
    [FromServices] AppDbContext ctx) =>
{
    var personagem = ctx.Personagens.Find(id);

    if (personagem == null)
    {
        return Results.NotFound("Personagem não encontrado");
    }

    var classe = ctx.Classes.Find(personagemAtualizado.ClasseId);

    if (classe == null)
    {
        return Results.BadRequest("Esta nova classe não existe");
    }

    personagem.ClasseId = personagemAtualizado.ClasseId;
    personagem.Nome = personagemAtualizado.Nome;
    personagem.Nivel = personagemAtualizado.Nivel;

    ctx.SaveChanges();

    return Results.Ok("Personagem atualizado");
});
```

### Explicação

1. O `id` é recebido pela URL.
2. O `Find(id)` procura o personagem que será alterado.
3. Se o personagem não existir, retornamos `404 Not Found`.
4. O sistema procura a classe informada no corpo da requisição.
5. Se a classe não existir, retornamos `400 Bad Request`.
6. Se os dados forem válidos, atualizamos as propriedades do personagem.
7. O `SaveChanges()` grava as alterações no banco.

### Por que não precisamos de Update()?

Como buscamos o personagem utilizando:

```csharp
var personagem = ctx.Personagens.Find(id);
```

O Entity Framework Core já acompanha esse objeto por meio do mecanismo chamado *Change Tracking*.

Quando modificamos suas propriedades, o Entity Framework consegue detectar as alterações.

Por isso, nesse caso, basta executar:

```csharp
ctx.SaveChanges();
```

Não é necessário chamar `ctx.Update(personagem)`.

### Exemplo de requisição

```http
PUT {{baseUrl}}/personagens/alterar/1
Content-Type: application/json

{
    "nome": "Pelison",
    "nivel": 35,
    "classeId": 2
}
```

O jogador pode alterar o nível ou a classe sem precisar obrigatoriamente mudar o nome.

---

## DELETE — Excluir personagem

A rota DELETE permite remover um personagem cadastrado utilizando seu ID.

```csharp
app.MapDelete("/personagens/excluir/{id}", (
    int id,
    [FromServices] AppDbContext ctx) =>
{
    var personagem = ctx.Personagens.Find(id);

    if (personagem == null)
    {
        return Results.NotFound("Personagem não encontrado!");
    }

    ctx.Personagens.Remove(personagem);
    ctx.SaveChanges();

    return Results.Ok("Personagem excluído com sucesso!");
});
```

### Explicação

- `Find(id)`: procura o personagem pelo identificador.
- `personagem == null`: verifica se o personagem não existe.
- `Results.NotFound()`: retorna o status HTTP `404`.
- `Remove(personagem)`: marca o personagem para exclusão.
- `SaveChanges()`: executa a exclusão no banco.
- `Results.Ok()`: retorna o status HTTP `200`, indicando sucesso.

O método `Remove()` não exclui imediatamente o registro do banco. A exclusão é efetivada quando executamos `SaveChanges()`.

### Exemplo de requisição

```http
DELETE {{baseUrl}}/personagens/excluir/1
```

Se o personagem existir, será excluído.

Se a requisição for executada novamente com o mesmo ID, a API deverá retornar `404 Not Found`, pois o personagem já não estará cadastrado.

---

## Aprendizados com o CRUD de Personagem

Durante a implementação, foram praticados os seguintes conceitos:

- Criação de endpoints com `MapPost()`, `MapGet()`, `MapPut()` e `MapDelete()`.
- Recebimento de dados com `[FromBody]`.
- Injeção de dependência com `[FromServices]`.
- Consulta de registros utilizando `Find()` e `Any()`.
- Validação da existência de classes antes de cadastrar ou alterar personagens.
- Utilização do relacionamento entre `Personagem` e `Classe`.
- Carregamento de entidades relacionadas utilizando `Include()`.
- Seleção de informações utilizando `Select()`.
- Atualização de entidades acompanhadas pelo Entity Framework Core.
- Exclusão de registros utilizando `Remove()`.
- Persistência das alterações com `SaveChanges()`.
- Utilização de respostas HTTP como `200 OK`, `400 Bad Request` e `404 Not Found`.

Com essa etapa, foram implementadas as quatro operações do CRUD de Personagem.

O próximo objetivo é utilizar os dados cadastrados em uma funcionalidade do sistema de builds, indo além das operações básicas de cadastro e consulta.

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
- CRUD completo de Classe.
- CRUD completo de Personagem.
- Validação da existência de classes durante o cadastro e a alteração de personagens.
- Consulta de personagens com informações das classes relacionadas.
- Estudo dos métodos `Find()`, `Any()`, `Include()`, `Select()`, `Add()`, `Remove()` e `SaveChanges()`.
- Arquivo de requisições HTTP para testes.
- Versionamento do projeto com Git e GitHub.

## Próximas etapas

- Testar e revisar os fluxos completos do CRUD de Personagem.
- Desenvolver funcionalidades envolvendo os equipamentos cadastrados.
- Utilizar personagens, classes e equipamentos em uma funcionalidade de montagem ou consulta de builds.
- Validar os requisitos da atividade.
- Preparar a demonstração do projeto.

