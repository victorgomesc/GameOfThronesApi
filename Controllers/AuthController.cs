using GameOfThronesAPI.Data;
using GameOfThronesAPI.DTOs;
using GameOfThronesAPI.Helpers;
using GameOfThronesAPI.Models;
using GameOfThronesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameOfThronesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(UserRegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email já cadastrado.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                AccessToken = token,
                Username = user.Username,
                Role = user.Role
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Credenciais inválidas.");

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                AccessToken = token,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
