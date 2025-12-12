using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicio_Create.Models;

namespace Servicio_Create.Controllers
{
    [Route("api/[controller]")]

    [Authorize]

    [ApiController]
    public class OcupacionController : Controller
    {
        private readonly DbLoginWsJwtContext _db;



        public OcupacionController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("AgregarOcupacion")]
        public async Task<IActionResult> AgregarOcupacion([FromBody] Class_Ocupaciones ocupacion)
        {
            if (ocupacion == null)
            {
                return BadRequest(new { mensaje = "Los datos de la ocupacion son inválidos." });
            }

            if (ocupacion.habitacion_ID <= 0 ||
                string.IsNullOrEmpty(ocupacion.Cedula_P) ||
                ocupacion.fecha_entrada == null ||
                ocupacion.fecha_salida == null ||
                ocupacion.credito_ID <= 0 ||
                string.IsNullOrEmpty(ocupacion.estado))
            {
                return BadRequest(new { mensaje = "Todos los campos obligatorios deben estar completos y ser válidos." });
            }

            try
            {


                // Agregar el producto a la base de datos
                await _db.Ocupacion.AddAsync(ocupacion);
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ocupacion agregada correctamente.", value = ocupacion });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al agregar ocupacion.", error = ex.InnerException?.Message ?? ex.Message });
            }

        }
    }
}
