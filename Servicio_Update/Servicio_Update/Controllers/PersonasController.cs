using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicio_Update.Models;

namespace Servicio_Update.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbLoginWsJwtContext _db;

        public PersonasController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpPut("ActualizarPersona/{cedula}")]
        public async Task<IActionResult> ActualizarPersona(string cedula, [FromBody] Class_Usuarios personaActualizada)
        {
            if (cedula != personaActualizada.Cedula_P)
            {
                return BadRequest(new { mensaje = "La cédula de la URL no coincide con la del cuerpo de la solicitud." });
            }

            var personaExistente = await _db.personas.FindAsync(cedula);
            if (personaExistente == null)
            {
                return NotFound(new { mensaje = "Persona no encontrada." });
            }

            // Actualizar los campos
            personaExistente.Nombre_P = personaActualizada.Nombre_P;
            personaExistente.Apellido_P = personaActualizada.Apellido_P;
            personaExistente.Telefono_P = personaActualizada.Telefono_P;
            personaExistente.Correo_P = personaActualizada.Correo_P;
            personaExistente.Fecha_Nacimiento_P = personaActualizada.Fecha_Nacimiento_P;
            personaExistente.Contrasenna_P = personaActualizada.Contrasenna_P;
            personaExistente.Rol_ID = personaActualizada.Rol_ID;

            try
            {
                await _db.SaveChangesAsync();
                return Ok(new { mensaje = "Persona actualizada correctamente." });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar persona.", detalle = ex.Message });
            }
        }
    }
}
