namespace constructora;

public class MaquinariaPesada : Equipo
{
    private double pesoToneladas;

    public double PesoToneladas { get => pesoToneladas; set => pesoToneladas = value; }

    public override double GetCostoDiario()
    {
        double costoFinal = CostoBase + PesoToneladas * 150;
        return costoFinal;
    }
}