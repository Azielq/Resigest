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
    public class OcupacionesController : ControllerBase
    {
        private readonly DbLoginWsJwtContext _db;

        public OcupacionesController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpPut("ActualizarOcupacion/{id}")]
        public async Task<IActionResult> ActualizarOcupacion(int id, [FromBody] Class_Ocupaciones ocupacionActualizada)
        {
            if (id != ocupacionActualizada.ocupacion_ID)
            {
                return BadRequest(new { mensaje = "El ID de ocupación no coincide con el cuerpo de la solicitud." });
            }

            var ocupacionExistente = await _db.ocupaciones.FindAsync(id);
            if (ocupacionExistente == null)
            {
                return NotFound(new { mensaje = "Ocupación no encontrada." });
            }

            // Actualizar campos permitidos
            ocupacionExistente.habitacion_ID = ocupacionActualizada.habitacion_ID;
            ocupacionExistente.Cedula_P = ocupacionActualizada.Cedula_P;
            ocupacionExistente.fecha_entrada = ocupacionActualizada.fecha_entrada;
            ocupacionExistente.fecha_salida = ocupacionActualizada.fecha_salida;
            ocupacionExistente.uso_credito = ocupacionActualizada.uso_credito;
            ocupacionExistente.credito_ID = ocupacionActualizada.credito_ID;
            ocupacionExistente.estado = ocupacionActualizada.estado;

            try
            {
                await _db.SaveChangesAsync();
                return Ok(new { mensaje = "Ocupación actualizada correctamente." });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar ocupación.", detalle = ex.Message });
            }
        }
    }
}
