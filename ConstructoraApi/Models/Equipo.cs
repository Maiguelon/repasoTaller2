namespace constructora;
using System.Text.Json.Serialization; 

// Esto permite guardar hijos dentro de una lista del padre
[JsonDerivedType(typeof(MaquinariaPesada), typeDiscriminator: "maquinaria")]
[JsonDerivedType(typeof(HerramientaMano), typeDiscriminator: "herramienta")]

public abstract class Equipo
{
    private int id;
    private string numeroSerie;
    private double costoBase;

    public int Id { get => id; set => id = value; }
    public string NumeroSerie { get => numeroSerie; set => numeroSerie = value; }
    public double CostoBase { get => costoBase; set => costoBase = value; }

    public abstract double GetCostoDiario();
}