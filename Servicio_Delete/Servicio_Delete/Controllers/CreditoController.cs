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
    public class CreditoController : Controller
    {
        private readonly DbLoginWsJwtContext _db;

        public CreditoController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpDelete]
        [Route("EliminarCredito/{credito_ID}")]
        public async Task<IActionResult> EliminarCredito(int credito_ID)
        {
            if (credito_ID <= 0)
            {
                return BadRequest(new { mensaje = "El ID proporcionado no es válido." });
            }

            try
            {
                var credito = await _db.creditos.FirstOrDefaultAsync(c => c.credito_ID == credito_ID);

                if (credito == null)
                {
                    return NotFound(new { mensaje = "El crédito con el ID especificado no existe." });
                }

                _db.creditos.Remove(credito);
                await _db.SaveChangesAsync();

                return Ok(new { mensaje = "Crédito eliminado correctamente.", value = credito });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el crédito.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

    }
}
