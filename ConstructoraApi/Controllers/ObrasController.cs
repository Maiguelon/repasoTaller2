using Microsoft.AspNetCore.Mvc;
using constructora;
namespace ConstructoraApi.Controllers;

using System.Text.Json;

[ApiController]
[Route("[controller]")]

public class ObrasController : ControllerBase
{
    private List<Obra> obras;
    private AccesoADatosObraJson ADObra;
    private AccesoADatosEquipoJson ADEquipo;

    // ruta para usar en el guardar
    string rutaObras = Path.Combine("Datos", "Obras.json");

    public ObrasController() // Constructor
    {
        ADObra = new AccesoADatosObraJson();
        obras = ADObra.CargarDatosObra(rutaObras);
        ADEquipo = new AccesoADatosEquipoJson();
    }

    // ---- GET ----
    [HttpGet("GetObras")]
    public ActionResult<List<Obra>> GetObras()
    {
        return Ok(obras); // devuelve la lista completa de obras
    }

    [HttpGet("GetInforme")]
    public ActionResult GetInforme()
    {
        // NO puedes hacer: obras.Equipamiento (Error: la lista no tiene equipamiento)
        // SI puedes hacer: obras.Select (Por cada obra, dame sus datos y calcula su costo)

        var informe = obras.Select(obra => new
        {
            Id = obra.Id,
            Nombre = obra.Nombre,
            // Aquí entramos a LA obra específica de esta iteración
            CantidadEquipos = obra.Equipamiento.Count,
            // Usamos Polimorfismo: Sumamos el costo de sus equipos
            CostoDiario = obra.Equipamiento.Sum(e => e.GetCostoDiario())
        });

        return Ok(informe);
    }

    // 3. PUT: Asignar un equipo a una obra existente
    [HttpPut("AsignarEquipo/{idObra}/{idEquipo}")]
    public ActionResult AsignarEquipo(int idObra, int idEquipo)
    {
        // 1. CARGAR CATÁLOGO: Buscamos el equipo en el inventario estático
        var inventario = ADEquipo.CargarDatosEquipo("Datos/Equipos.json");
        var equipoEncontrado = inventario.FirstOrDefault(e => e.Id == idEquipo);

        if (equipoEncontrado == null)
        {
            return NotFound($"El equipo con ID {idEquipo} no existe en el catálogo.");
        }

        // 2. BUSCAR OBRA: Buscamos la obra donde lo queremos asignar
        var obraAEditar = obras.FirstOrDefault(o => o.Id == idObra);

        if (obraAEditar == null)
        {
            return NotFound($"La obra con ID {idObra} no existe.");
        }

        // 3. ASIGNAR: Agregamos el objeto real que sacamos del catálogo
        // (Aquí ya no hay riesgo de datos falsos porque vienen de nuestro JSON interno)
        obraAEditar.Equipamiento.Add(equipoEncontrado);

        // 4. GUARDAR: Persistimos el cambio en el archivo de obras
        ADObra.GuardarObra(rutaObras, obras);

        return Ok($"Equipo '{equipoEncontrado.NumeroSerie}' asignado a la obra '{obraAEditar.Nombre}'.");
    }

    // ---- Post ----
    [HttpPost("AgregarObra")]
    public ActionResult<Obra> AgregarObra([FromBody] Obra nuevaObra)
    {
        if (nuevaObra.FechaInicio < new DateTime(2024, 1, 1))
        {
            return BadRequest("La fecha de inicio no puede ser anterior al 2024.");
        }

        // Asignar ID Automático
        int nuevoId = obras.Count > 0 ? obras.Max(o => o.Id) + 1 : 1;
        nuevaObra.Id = nuevoId;

        // Guardado (Esto lo tenías perfecto)
        obras.Add(nuevaObra);
        ADObra.GuardarObra(rutaObras, obras);

        // OPCIÓN MÁS SIMPLE (Si te lías con CreatedAtAction):
        return Created("", nuevaObra);
    }

    // ---- Delete ----
    [HttpDelete("EliminarObra/{id}")]
    public ActionResult EliminarObra(int id)
    {
        // 1. Buscamos la obra en la lista de memoria
        var obraAEliminar = obras.FirstOrDefault(o => o.Id == id);

        if (obraAEliminar == null)
        {
            return NotFound("No se existe una obra con ese ID.");
        }

        // 2. La quitamos de la lista
        obras.Remove(obraAEliminar);

        // 3. Persistimos el cambio (sobrescribimos el JSON con la lista reducida)
        ADObra.GuardarObra(rutaObras, obras);

        return Ok($"La obra {id} fue eliminada correctamente.");
    }
}