using System.Text.Json;
namespace constructora;

public class AccesoADatosEquipoJson
{
    public List<Equipo> CargarDatosEquipo(string archivo)
    {
        if (!File.Exists(archivo))
        {
            return new List<Equipo>();
        }
        string linea = File.ReadAllText(archivo);
        List<Equipo> equipos = JsonSerializer.Deserialize<List<Equipo>>(linea);
        return equipos ?? new List<Equipo>();
    }
}