using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using SistemaBuild.API.Data;
using SistemaBuild.API.Modelo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok(" ==== SISTMA DE BUILDS ====");
});

// -- LISTAR
app.MapGet("/classes/listar", (
    [FromServices] AppDbContext ctx) =>
{
    if (ctx.Classes.Count() == 0)
    {
        return Results.BadRequest("A Lista está vazia!");
    }
    return Results.Ok(ctx.Classes.ToList());
});

// -- CADASTRO
app.MapPost("/classes/cadastro", (
    [FromBody]Classe classe, [FromServices]AppDbContext ctx) =>
{
    var existe = ctx.Classes.Any(c => c.Nome == classe.Nome);
    if (existe)
    {
        return Results.BadRequest("Essa classe já está cadastrada!");
    }
    ctx.Classes.Add(classe);
    ctx.SaveChanges();

    return Results.Ok("Classe criada com sucesso");
});

// -- DELETE
app.MapDelete("/classes/excluir/{id}", (
    int id,
    [FromServices] AppDbContext ctx
    ) =>
{
    var classe = ctx.Classes.Find(id);
    if (classe == null)
    {
        return Results.NotFound("Classe não Encontrada");
    }
    ctx.Classes.Remove(classe);
    ctx.SaveChanges();
    return Results.Ok("Classe excluída com sucesso");
});

// UpDATE
app.MapPut("/classes/alterar/{id}", (
    int id,
    [FromBody]Classe classeAtualizada,
    [FromServices] AppDbContext ctx) =>
{
    var classe = ctx.Classes.Find(id);
    if (classe == null)
    {
        return Results.NotFound("Classe não Encontrada");
    }
    
    if (classe.Nome != classeAtualizada.Nome)
    {
        classe.Nome = classeAtualizada.Nome;
        ctx.Classes.Update(classe);
        ctx.SaveChanges();

        return Results.Ok("Classe alterada com sucesso!");
    }

    return Results.Ok("O nome informado é igual ao atual. Nenhuma alteração realizada.");
});

// ---- PERSONAGEM
app.MapPost("/personagens/cadastrar", (
    [FromBody]Personagem personagem,
    [FromServices] AppDbContext ctx) =>
{
    var existe = ctx.Classes.Any(c => c.Id == personagem.ClasseId);
    if (!existe)
    {
        return Results.BadRequest("Essa classe não existe");
    }
    ctx.Personagens.Add(personagem);
    ctx.SaveChanges();
    
    return Results.Ok("Personagem criado com sucesso, Bom jogo!!");
});

// -- Listar Personagens Cadastrados
app.MapGet("/personagens/listar", (
    [FromServices] AppDbContext ctx) =>
{
    if (!ctx.Personagens.Any())
    {
        return Results.BadRequest("Não há personagens cadastrados");
    }

    return Results.Ok(ctx.Personagens
        .Include(p => p.Classe)
        .Select(p => new
        {
            p.Id,
            p.Nome,
            p.Nivel,
            Classe = p.Classe!.Nome
        })
        .ToList());
});

// -- Alterar Personagem
app.MapPut("personagens/alterar/{id}", (
    int id,
    [FromBody] Personagem personagemAtualizado,
    [FromServices] AppDbContext ctx) =>
{
    var personagem = ctx.Personagens.Find(id);
    if (personagem == null)
    {
        return Results.NotFound("Não Existe esse personagem cadastrado");
    }
   
    var classe = ctx.Classes.Find(personagemAtualizado.ClasseId);
    if (classe != null)
    {
        personagem.ClasseId = personagemAtualizado.ClasseId;
        personagem.Nome = personagemAtualizado.Nome;
        personagem.Nivel = personagemAtualizado.Nivel;
        ctx.Update(personagem);
        ctx.SaveChanges();
        return Results.Ok("Personagem Atualizado");
    }
    else
    {
        return Results.BadRequest("Esta nova Classe não existe");
    }
});

// -- DELETE PERSONAGEM
app.MapDelete("personagens/excluir/{id}", (
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
        return Results.Ok("Personagem deletado com sucesso!");
    }
);

// --- EQUIPAMENTOS
app.MapPost("/equipamentos/cadastrar", (
    [FromBody]Equipamento equipamento,
    [FromServices] AppDbContext ctx) => 
    {
        var existe = ctx.Equipamentos.Any(x => x.Nome == equipamento.Nome);
        if (!existe)
        {
            if(equipamento.Ataque < 0 || equipamento.Defesa < 0)
            {
               return Results.BadRequest("Valores de ataque e defesa, não podem ser negativos."); 
            }
            ctx.Equipamentos.Add(equipamento);
            ctx.SaveChanges();
            return Results.Ok("Equipamento Cadastrado");
        }
        else
        {
            return Results.BadRequest("Já existe um equipamento com esse nome");            
        }
    }
);
app.Run();
