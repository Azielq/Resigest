using Microsoft.AspNetCore.Authorization;
using Servicio_Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Servicio_Read.Controllers
{
    [Route("api/[controller]")]

    [Authorize]


    [ApiController]
    public class PersonaController : Controller
    {
        private readonly DbLoginWsJwtContext _bd;

        public PersonaController(DbLoginWsJwtContext bd)
        {
            this._bd = bd;
        }

        [HttpGet]
        [Route("ListaPersonas")]

        public async Task<IActionResult> ListasPersonas()
        {
            var listapersonas = await _bd.personas.ToListAsync();

            if (listapersonas == null || listapersonas.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se logro encontrar personas" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = listapersonas });
        }

        
        [HttpGet]
        [Route("BuscarPersonaPorCedula/{CedulaP}")]
        public async Task<IActionResult> BuscarPersonaPorCedula(string CedulaP)
        {
            var persona = await _bd.personas.FindAsync(CedulaP);

            if (persona == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se ha logrado encontrar a la persona que buscas" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = persona });
        }

    }
}
