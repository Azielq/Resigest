using Microsoft.AspNetCore.Authorization;
using Servicio_Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Servicio_Read.Controllers
{
    [Route("api/[controller]")]

    [Authorize]


    [ApiController]
    public class OcupacionController : Controller
    {
        private readonly DbLoginWsJwtContext _bd;

        public OcupacionController(DbLoginWsJwtContext bd)
        {
            this._bd = bd;
        }

        [HttpGet]
        [Route("ListaOcupaciones")]

        public async Task<IActionResult> ListaOcupaciones()
        {
            var listaOcupaciones = await _bd.ocupaciones.ToListAsync();

            if (listaOcupaciones == null || listaOcupaciones.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se logro encontrar" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = listaOcupaciones});
        }
        [HttpGet]
        [Route("BuscarOcupacionPorId/{id}")]
        public async Task<IActionResult> BuscarOcupacionPorId(int id)
        {
            var ocupacion = await _bd.ocupaciones.FirstOrDefaultAsync(h => h.ocupacion_ID == id);

            if (ocupacion == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se ha encontrado la habitación con ese ID." });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = ocupacion });
        }

    }
}
