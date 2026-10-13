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

app.Run();
