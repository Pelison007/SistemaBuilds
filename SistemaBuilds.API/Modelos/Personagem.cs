namespace SistemaBuild.API.Modelo;

public class Personagem
{
    public int Id {get; set;}
    public int ClasseId { get; set; }
    public string Nome {get;set;} = string.Empty;
    public int Nivel {get;set;}
}