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
    public class UsuarioController : Controller
    {
        private readonly DbLoginWsJwtContext _db;

        public UsuarioController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpDelete]
        [Route("EliminarUsuario/{Cedula_P}")]
        public async Task<IActionResult> EliminarCredito(string Cedula_P)
        {
            if (Cedula_P == null)
            {
                return BadRequest(new { mensaje = "El ID proporcionado no es válido." });
            }

            try
            {
                var Usuario = await _db.personas.FirstOrDefaultAsync(c => c.Cedula_P == Cedula_P);

                if (Usuario == null)
                {
                    return NotFound(new { mensaje = "El Usuario con el ID especificado no existe." });
                }

                _db.personas.Remove(Usuario);
                await _db.SaveChangesAsync();

                return Ok(new { mensaje = "Usuario eliminado correctamente.", value = Usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el Usuario.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
