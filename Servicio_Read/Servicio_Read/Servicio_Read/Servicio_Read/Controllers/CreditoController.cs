using Microsoft.AspNetCore.Authorization;
using Servicio_Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace Servicio_Read.Controllers
{
    

    [Route("api/[controller]")]

    [Authorize]

    [ApiController]
    public class CreditoController : Controller
    {
        private readonly DbLoginWsJwtContext _bd;

        public CreditoController(DbLoginWsJwtContext bd)
        {
            this._bd = bd;
        }

        [HttpGet]
        [Route("ListaCredito")]

        public async Task<IActionResult> ListaCredito()
        {
            var listacredito = await _bd.creditos.ToListAsync();

            if (listacredito == null || listacredito.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se logro encontrar los creditos" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = listacredito });
        }



        [HttpGet]
        [Route("BuscarCredito/{CedulaP}")]
        public async Task<IActionResult> BuscarCredito(string CedulaP)
        {
            var creditos = await _bd.creditos.Where(c => c.Cedula_P == CedulaP).ToListAsync();

            if (creditos == null || creditos.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se ha logrado encontrar ningún crédito con la cédula digitada" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = creditos });
        }

        [HttpGet]
        [Route("BuscarCreditoPorId/{credito_ID}")]
        public async Task<IActionResult> BuscarCreditoPorId(int credito_ID)
        {
            //var creditos = await _bd.creditos.Where(c => c.credito_ID == credito_ID).ToListAsync(); ////Original
            var credito = await _bd.creditos.FirstOrDefaultAsync(c => c.credito_ID == credito_ID); //Prueba Cris

            if (credito == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se ha logrado encontrar ningún crédito con el ID digitado" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = credito });
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
