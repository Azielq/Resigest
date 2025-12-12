using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicio_Delete.Models;

namespace Servicio_Delete.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MovimientoCreditoController : Controller
    {
        private readonly DbLoginWsJwtContext _db;

        public MovimientoCreditoController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpDelete]
        [Route("EliminarMovCredito/{movimiento_ID}")]
        public async Task<IActionResult> EliminarMovCredito(int movimiento_ID)
        {
            if (movimiento_ID <= 0)
            {
                return BadRequest(new { mensaje = "El ID proporcionado no es válido." });
            }

            try
            {
                var movimiento = await _db.movimientosCredito.FirstOrDefaultAsync(m => m.movimiento_ID == movimiento_ID);

                if (movimiento == null)
                {
                    return NotFound(new { mensaje = "El movimiento de crédito con el ID especificado no existe." });
                }

                _db.movimientosCredito.Remove(movimiento);
                await _db.SaveChangesAsync();

                return Ok(new { mensaje = "Movimiento de crédito eliminado correctamente.", value = movimiento });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar movimiento de crédito.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


    }
}
