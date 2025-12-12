using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicio_Create.Models;

namespace Servicio_Create.Controllers
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

        [HttpPost]
        [Route("AgregarCredito")]
        public async Task<IActionResult> AgregarCredito([FromBody] Class_Credito credito)
        {
            if (credito == null)
            {
                return BadRequest(new { mensaje = "Los datos del credito son inválidos." });
            }

            if (string.IsNullOrEmpty(credito.Cedula_P) ||
                credito.monto_maximo <= 0 ||
                credito.saldo_actual < 0 ||
                credito.fecha_vencimiento == null ||
                string.IsNullOrEmpty(credito.estado))
            {
                return BadRequest(new { mensaje = "Todos los campos obligatorios deben estar completos y ser válidos." });
            }

            try
            {

                var Cedulaexiste = await _db.personas.AnyAsync(p => p.Cedula_P == credito.Cedula_P);
                if (!Cedulaexiste)
                {
                    return BadRequest(new { mensaje = "La cédula ingresada no existe. Debe utilizar una cédula válida registrada." });
                }
                // Agregar el producto a la base de datos
                await _db.creditos.AddAsync(credito);
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "credito agregado correctamente.", value = credito });
            }
            catch (Exception ex)
            {
                 return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al agregar credito.", error = ex.InnerException?.Message ?? ex.Message });
            }
 
        }
    }
}