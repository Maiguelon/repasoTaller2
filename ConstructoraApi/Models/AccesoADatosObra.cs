using System.Text.Json;
namespace constructora;

public class AccesoADatosObraJson 
{
    public List<Obra> CargarDatosObra(string archivo)
    {
        if (!File.Exists(archivo))
        {
            return new List<Obra>();
        }   
        string linea = File.ReadAllText(archivo);
        List<Obra> obras = JsonSerializer.Deserialize<List<Obra>>(linea);
        return obras ?? new List<Obra>();
    }

    public void GuardarObra(string archivo, List<Obra> obras)
    {
        var opciones = new JsonSerializerOptions {WriteIndented = true};
        string json = JsonSerializer.Serialize(obras, opciones);
        File.WriteAllText(archivo, json);
    }
}