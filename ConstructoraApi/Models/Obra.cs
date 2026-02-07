namespace constructora;

public class Obra
{
    private int id;
    private string nombre;
    private string direccion;
    private DateTime fechaInicio;
    private List<Equipo> equipamiento;

    public Obra(List<Equipo> equipamiento)
    {
        this.equipamiento = new List<Equipo>();
    }

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public List<Equipo> Equipamiento { get => equipamiento; set => equipamiento = value; }
}