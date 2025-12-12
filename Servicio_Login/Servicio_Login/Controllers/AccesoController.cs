using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Servicio_Login.Models;
using Servicio_Login.Encripta;
using Servicio_Login.Models.Login;
using Microsoft.EntityFrameworkCore;


namespace Servicio_REST_SQL_login_JWT.Controllers
{
    [Route("api/[Controller]")]

    [AllowAnonymous]

    [ApiController]

    public class AccesoController : Controller
    {
        private readonly DbLoginWsJwtContext _bd;
        private readonly Utilidades _utilidades;

        public AccesoController(DbLoginWsJwtContext bd, Utilidades utilidades)
        {
            this._bd = bd;
            this._utilidades = utilidades;
        }

        [HttpPost]
        [Route("Registrarse")]

        public async Task<IActionResult> Registrarse(classUser obj)
        {
            var correoExistente = await _bd.TbUsuarios.AnyAsync(u => u.Correo_P == obj.Correo_P);
            if (correoExistente)
            {
                return BadRequest(new { isSuccess = false, message = "Ya hay una cuenta con ese correo." });
            }
            var modeloUsuario = new Class_Usuario
            {
                Cedula_P = obj.Cedula_P,
                Nombre_P = obj.Nombre_P,
                Apellido_P = obj.Apellido_P,
                Telefono_P = obj.Telefono_P,
                Correo_P = obj.Correo_P,
                Fecha_Nacimiento_P = obj.Fecha_Nacimiento_P,
                Fecha_Registro_P = obj.Fecha_Registro_P,
                Contrasenna_P = _utilidades.EncriptarSHA256(obj.Contrasenna_P),
                Rol_ID = obj.Rol_ID,
            };

            await _bd.TbUsuarios.AddAsync(modeloUsuario);
            await _bd.SaveChangesAsync();

            if (modeloUsuario.Cedula_P != null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(ClassLogin objeto)
        {
            var usuarioEncontrado = await _bd.TbUsuarios.Where(u => u.Correo_P == objeto.Correo_P && u.Contrasenna_P == _utilidades.EncriptarSHA256(objeto.Contrasenna_P)).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.GenerarJWT(usuarioEncontrado) });
            }
        }
    }
}