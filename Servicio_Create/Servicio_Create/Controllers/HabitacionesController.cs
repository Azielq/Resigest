using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicio_Create.Models;

namespace Servicio_Create.Controllers
{
    [Route("api/[controller]")]

    [Authorize]

    [ApiController]
    public class HabitacionesController : Controller
    {
        private readonly DbLoginWsJwtContext _db;



        public HabitacionesController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("AgregarHabitacion")]
        public async Task<IActionResult> AgregarHabitacion([FromBody] Class_Habitaciones Habitacion)
        {
            if (Habitacion == null)
            {
                return BadRequest(new { mensaje = "Los datos de la Habitacion son inválidos." });
            }

            if (string.IsNullOrEmpty(Habitacion.numero) ||
                Habitacion.tipo_ID <= 0 ||
                string.IsNullOrEmpty(Habitacion.estado))
            {
                return BadRequest(new { mensaje = "Todos los campos obligatorios deben estar completos y ser válidos." });
            }

            try
            {

                
                // Agregar el producto a la base de datos
                await _db.Habitacion.AddAsync(Habitacion);
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Habitacion agregada correctamente.", value = Habitacion });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al agregar Habitacion.", error = ex.InnerException?.Message ?? ex.Message });
            }

        }
    }
}
