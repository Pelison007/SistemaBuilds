using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using SistemaBuild.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/", () =>
{
    return Results.Ok(" ==== SISTMA DE BUILDS ====");
});

app.MapGet("/classes/listar", (
    [FromServices] AppDbContext ctx) =>
{
    if (ctx.Classes.Count() == 0)
    {
        return Results.BadRequest("A Lista está vazia!");
    }
    return Results.Ok(ctx.Classes.ToList());
});



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
