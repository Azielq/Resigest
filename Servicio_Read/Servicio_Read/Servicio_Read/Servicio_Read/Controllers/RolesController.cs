using Microsoft.AspNetCore.Authorization;
using Servicio_Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Servicio_Read.Controllers
{
    [Route("api/[controller]")]

    [Authorize]

    [ApiController]
    public class RolesController : Controller
    {
        private readonly DbLoginWsJwtContext _bd;

        public RolesController(DbLoginWsJwtContext bd)
        {
            this._bd = bd;
        }

        [HttpGet]
        [Route("ListaRoles")]

        public async Task<IActionResult> ListaRoles()
        {
            var listaRoles = await _bd.roles.ToListAsync();

            if (listaRoles == null || listaRoles.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se logro encontrar los roles" });
            }

            return StatusCode(StatusCodes.Status200OK, new { value = listaRoles });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
