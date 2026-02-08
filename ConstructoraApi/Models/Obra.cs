namespace constructora;

public class Obra
{
    // 1. Auto-Properties: Más limpio, menos código, menos errores.
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public DateTime FechaInicio { get; set; }
    
    // 2. INICIALIZACIÓN OBLIGATORIA:
    // Al poner "= new List<Equipo>();" aquí, garantizas que JAMÁS sea null,
    // incluso si el JSON no trae nada.
    public List<Equipo> Equipamiento { get; set; } = new List<Equipo>();

    // 3. Constructor vacío:
    // Necesario para que el deserializador pueda crear la instancia sin parámetros.
    public Obra() { }
}