namespace constructora;

public class HerramientaMano : Equipo
{
    public enum tipo_herramienta
    {
        Electrica = 1,
        Manual = 2
    }

    private tipo_herramienta tipo;
    private int aniosUso;

    public tipo_herramienta Tipo { get => tipo; set => tipo = value; }
    public int AniosUso { get => aniosUso; set => aniosUso = value; }

    public override double GetCostoDiario()
    {
        double costoFinal = CostoBase;
        if (Tipo == tipo_herramienta.Electrica && AniosUso > 5)
        {
            costoFinal *= 0.9;
        };

        return costoFinal;
    }
}