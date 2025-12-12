using Microsoft.AspNetCore.Authorization;
using Servicio_Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Servicio_Read.Controllers
{

    
    [Route("api/[controller]")]

    [Authorize]


    [ApiController]
    public class MovimientoCreditosController : Controller
    {
        private readonly DbLoginWsJwtContext _bd;

        public MovimientoCreditosController(DbLoginWsJwtContext bd)
        {
            this._bd = bd;
        }

        [HttpGet]
        [Route("ListaMovimientos")]

        public async Task<IActionResult> ListaMovimientos()
        {
            var listaMovimientos = await _bd.movimientosCredito.ToListAsync();

            if (listaMovimientos == null || listaMovimientos.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se logro encontrar los movimientos de los creditos" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = listaMovimientos });
        }

        [HttpGet("BuscarMovimientoPorId/{id}")]
        public async Task<IActionResult> BuscarMovimientoPorId(int id)
        {
            var movimiento = await _bd.movimientosCredito.FirstOrDefaultAsync(m => m.movimiento_ID == id);

            if (movimiento == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se ha encontrado el movimiento con ese ID." });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = movimiento });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
