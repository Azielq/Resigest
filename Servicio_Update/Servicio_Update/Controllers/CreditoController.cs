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

    public class CreditoController : ControllerBase
    {
        private readonly DbLoginWsJwtContext _db;

        public CreditoController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpPut("ActualizarCredito/{id}")]
        public async Task<IActionResult> ActualizarCredito(int id, [FromBody] Class_Credito creditoActualizado)
        {
            if (id != creditoActualizado.credito_ID)
            {
                return BadRequest(new { mensaje = "El ID proporcionado no coincide con el cuerpo de la solicitud." });
            }

            var creditoExistente = await _db.creditos.FindAsync(id);
            if (creditoExistente == null)
            {
                return NotFound(new { mensaje = "Crédito no encontrado." });
            }

            // Actualizar campos
            creditoExistente.Cedula_P = creditoActualizado.Cedula_P;
            creditoExistente.monto_maximo = creditoActualizado.monto_maximo;
            creditoExistente.saldo_actual = creditoActualizado.saldo_actual;
            creditoExistente.fecha_vencimiento = creditoActualizado.fecha_vencimiento;
            creditoExistente.estado = creditoActualizado.estado;

            try
            {
                await _db.SaveChangesAsync();
                return Ok(new { mensaje = "Crédito actualizado correctamente." });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el crédito.", detalle = ex.Message });
            }
        }
    }
}
