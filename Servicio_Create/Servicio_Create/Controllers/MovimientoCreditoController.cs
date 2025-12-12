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


    public class MovimientoCreditoController : Controller
    {

        private readonly DbLoginWsJwtContext _db;



        public MovimientoCreditoController(DbLoginWsJwtContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("AgregarMovCredito")]
        public async Task<IActionResult> AgregarMovCredito([FromBody] Class_MovimientosCredito Movcredito)
        {
            if (Movcredito == null)
            {
                return BadRequest(new { mensaje = "Los datos del movimiento de credito son inválidos." });
            }

            if (Movcredito.credito_ID <= 0 ||
                string.IsNullOrEmpty(Movcredito.tipo_movimiento) ||
                Movcredito.monto <= 0 ||
                Movcredito.fecha_movimiento == null)
            {
                return BadRequest(new { mensaje = "Todos los campos obligatorios deben estar completos y ser válidos." });
            }


            try
            {
                var Creditoexiste = await _db.creditos.AnyAsync(p => p.credito_ID == Movcredito.credito_ID);
                if (!Creditoexiste)
                {
                    return BadRequest(new { mensaje = "El credito ingresado no existe. Debe utilizar una credito válido registrado." });
                }
                // Agregar el producto a la base de datos
                await _db.movimientosCredito.AddAsync(Movcredito);
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Movimiento de credito agregado correctamente.", value = Movcredito });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al agregar movimiento.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}

