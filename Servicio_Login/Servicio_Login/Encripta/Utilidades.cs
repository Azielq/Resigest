using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Servicio_Login.Models;


namespace Servicio_Login.Encripta
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;

        public Utilidades(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string EncriptarSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string GenerarJWT(Class_Usuario modelo)
        {
            if (modelo == null)
            {
                throw new ArgumentNullException(nameof(modelo), "El modelo de usuario no puede ser nulo.");
            }

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.Cedula_P.ToString()),
                new Claim(ClaimTypes.Email, modelo.Correo_P!)
            };

            string jwtKey = _configuration["Jwt:key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("La clave JWT no está configurada correctamente.");
            }

            var secutiryKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(secutiryKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
