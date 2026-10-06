namespace SistemaBuild.API.Modelo;

public class Equipamento
{
    public int Id {get;set;}
    public string Nome {get;set;} = string.Empty;
    public string Tipo {get;set;} = string.Empty;
    public int Ataque {get;set;}
    public int Defesa {get;set;}
}