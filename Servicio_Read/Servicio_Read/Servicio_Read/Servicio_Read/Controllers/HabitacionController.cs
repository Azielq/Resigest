using Microsoft.AspNetCore.Authorization;
using Servicio_Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Servicio_Read.Controllers
{

    [Route("api/[controller]")]

    [Authorize]

    [ApiController]
    public class HabitacionController : Controller
    {
        private readonly DbLoginWsJwtContext _bd;

        public HabitacionController(DbLoginWsJwtContext bd)
        {
            this._bd = bd;
        }

        [HttpGet]
        [Route("ListaHabitaciones")]

        public async Task<IActionResult> ListaHabitaciones()
        {
            var listahabitaciones = await _bd.habitaciones.ToListAsync();

            if (listahabitaciones == null || listahabitaciones.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se logro encontrar las habitaciones" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = listahabitaciones });
        }

        [HttpGet]
        [Route("BuscarHabitacion/{Numero}")]
        public async Task<IActionResult> BuscarHabitacion(string Numero)
        {
            var habitacion = await _bd.habitaciones.FirstOrDefaultAsync(h => h.numero == Numero);

            if (habitacion == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se ha encontrado la habitación con ese número." });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = habitacion });
        }

        [HttpGet]
        [Route("BuscarHabitacionPorId/{id}")]
        public async Task<IActionResult> BuscarHabitacionPorId(int id)
        {
            var habitacion = await _bd.habitaciones.FirstOrDefaultAsync(h => h.habitacion_ID == id);

            if (habitacion == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se ha encontrado la habitación con ese ID." });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = habitacion });
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
