using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicio_Delete.Models;

namespace Servicio_Delete.Controllers
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

        [HttpDelete]
        [Route("EliminarOcupacion/{ocupacion_ID}")]
        public async Task<IActionResult> EliminarOcupacion(int ocupacion_ID)
        {
            if (ocupacion_ID <= 0)
            {
                return BadRequest(new { mensaje = "El ID proporcionado no es válido." });
            }

            try
            {
                var ocupacionExistente = await _db.Ocupacion
                    .FirstOrDefaultAsync(o => o.ocupacion_ID == ocupacion_ID);

                if (ocupacionExistente == null)
                {
                    return NotFound(new { mensaje = "La ocupación con el ID especificado no existe." });
                }

                // Eliminar la ocupación de la base de datos
                _db.Ocupacion.Remove(ocupacionExistente);
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ocupación eliminada correctamente.", value = ocupacionExistente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al eliminar ocupación.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
