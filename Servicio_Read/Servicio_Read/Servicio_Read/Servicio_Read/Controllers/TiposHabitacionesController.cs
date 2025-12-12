using Microsoft.AspNetCore.Authorization;
using Servicio_Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace Servicio_Read.Controllers
{
    [Route("api/[controller]")]

    [Authorize]


    [ApiController]
    public class TiposHabitacionesController : Controller
    {
        private readonly DbLoginWsJwtContext _bd;

        public TiposHabitacionesController(DbLoginWsJwtContext bd)
        {
            this._bd = bd;
        }

        [HttpGet]
        [Route("ListaTiposHabitaciones")]

        public async Task<IActionResult> ListaTiposHabitaciones()
        {
            var listatiposhabitaciones = await _bd.tipohabitacion.ToListAsync();

            if (listatiposhabitaciones == null || listatiposhabitaciones.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se logro encontrar los tipos de habitaciones" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = listatiposhabitaciones });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
