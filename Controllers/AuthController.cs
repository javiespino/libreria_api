using javier_api.DTOs;
using javier_api.Models;
using javier_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace javier_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IUsuarioRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            if (await _repo.ExistsAsync(dto.Username, dto.Email))
                return Conflict(new { message = "El usuario o email ya existe." });

            var usuario = new Usuario
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "user"
            };

            await _repo.CreateAsync(usuario);
            return Ok(new { message = "Usuario registrado correctamente." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var usuario = await _repo.GetByUsernameAsync(dto.Username);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                return Unauthorized(new { message = "Credenciales incorrectas." });

            var token = GenerarToken(usuario);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Username = usuario.Username,
                Role = usuario.Role
            });
        }

        private string GenerarToken(Usuario usuario)
        {
            var jwtKey = _config["Jwt:Key"]!;
            var jwtIssuer = _config["Jwt:Issuer"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name,           usuario.Username),
                new Claim(ClaimTypes.Role,           usuario.Role),
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}