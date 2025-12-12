using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicio_Delete.Models;

namespace Servicio_Delete.Controllers
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

        [HttpDelete]
        [Route("EliminarHabitacion/{Habitacion_ID}")]
        public async Task<IActionResult> EliminarHabitacion(int Habitacion_ID)
        {
            if (Habitacion_ID <= 0)
            {
                return BadRequest(new { mensaje = "El ID proporcionado no es válido." });
            }

            try
            {
                var habitacion = await _db.Habitacion.FirstOrDefaultAsync(h => h.Habitacion_ID == Habitacion_ID);

                if (habitacion == null)
                {
                    return NotFound(new { mensaje = "La habitación con el ID especificado no existe." });
                }

                _db.Habitacion.Remove(habitacion);
                await _db.SaveChangesAsync();

                return Ok(new { mensaje = "Habitación eliminada correctamente.", value = habitacion });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar la habitación.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

    }
}
