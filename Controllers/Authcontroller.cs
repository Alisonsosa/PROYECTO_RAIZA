using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RAIZA.Interfaces;
using RAIZA.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RAIZA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration, IUsuarioI usuarioRepo) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUsuarioI _usuarioRepo = usuarioRepo;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Solicitud de cliente inválida.");
            }

            var usuario = await _usuarioRepo.GetUsuarioByCorreo(login.Email);

            if (usuario == null || usuario.ContrasenaHash != login.Password)
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
        }
    }
}