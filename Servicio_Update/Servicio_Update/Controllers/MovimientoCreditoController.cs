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

    public class MovimientosCreditoController : ControllerBase
    {
        private readonly DbLoginWsJwtContext _db;

        public MovimientosCreditoController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpPut("ActualizarMovimiento/{id}")]
        public async Task<IActionResult> ActualizarMovimiento(int id, [FromBody] Class_MovimientosCredito movimientoActualizado)
        {
            if (id != movimientoActualizado.movimiento_ID)
            {
                return BadRequest(new { mensaje = "El ID de movimiento no coincide con el cuerpo de la solicitud." });
            }

            var movimientoExistente = await _db.movimientosCredito.FindAsync(id);
            if (movimientoExistente == null)
            {
                return NotFound(new { mensaje = "Movimiento no encontrado." });
            }

            // Actualizar campos permitidos
            movimientoExistente.credito_ID = movimientoActualizado.credito_ID;
            movimientoExistente.tipo_movimiento = movimientoActualizado.tipo_movimiento;
            movimientoExistente.monto = movimientoActualizado.monto;
            movimientoExistente.fecha_movimiento = movimientoActualizado.fecha_movimiento;
            movimientoExistente.descripcion = movimientoActualizado.descripcion;
            

            try
            {
                await _db.SaveChangesAsync();
                return Ok(new { mensaje = "Movimiento actualizado correctamente." });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar movimiento.", detalle = ex.Message });
            }
        }
    }
}
