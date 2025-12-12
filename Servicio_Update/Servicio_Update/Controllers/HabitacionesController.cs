using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicio_Update.Models;

namespace Servicio_Update.Controllers
{
    [Route("api/[controller]")]

    [Authorize]

    [ApiController]

    public class HabitacionesController : ControllerBase
    {
        private readonly DbLoginWsJwtContext _db;

        public HabitacionesController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpPut("ActualizarHabitacion/{id}")]
        public async Task<IActionResult> ActualizarHabitacion(int id, [FromBody] Class_Habitaciones habitacionActualizada)
        {
            if (id != habitacionActualizada.habitacion_ID)
            {
                return BadRequest(new { mensaje = "El ID de la habitación no coincide con el cuerpo de la solicitud." });
            }

            var habitacionExistente = await _db.habitaciones.FindAsync(id);
            if (habitacionExistente == null)
            {
                return NotFound(new { mensaje = "Habitación no encontrada." });
            }

            // Actualizar campos
            habitacionExistente.numero = habitacionActualizada.numero;
            habitacionExistente.tipo_ID = habitacionActualizada.tipo_ID;
            habitacionExistente.estado = habitacionActualizada.estado;

            try
            {
                await _db.SaveChangesAsync();
                return Ok(new { mensaje = "Habitación actualizada correctamente." });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar la habitación.", detalle = ex.Message });
            }
        }
    }
}
