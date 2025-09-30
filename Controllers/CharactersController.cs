using GameOfThronesAPI.Data;
using GameOfThronesAPI.DTOs;
using GameOfThronesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GameOfThronesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<object>> GetAll(
            [FromQuery] PaginationParams pagination,
            [FromQuery] int? houseId,
            [FromQuery] string? status,
            [FromQuery] string? nome)
        {
            var query = _context.Characters
                .Include(c => c.House)
                .Include(c => c.NovaCasa)
                .Include(c => c.Fortaleza)
                .AsQueryable();

            if (houseId.HasValue)
                query = query.Where(c => c.HouseId == houseId.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(c => c.Status.ToLower() == status.ToLower());

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            var total = await query.CountAsync();

            var characters = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(c => new CharacterDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Titulo = c.Titulo,
                    Status = c.Status,
                    Descricao = c.Descricao,
                    Sexo = c.Sexo,
                    Casa = c.House != null ? c.House.Nome : null,
                    NovaCasa = c.NovaCasa != null ? c.NovaCasa.Nome : null,
                    Fortaleza = c.Fortaleza != null ? c.Fortaleza.Nome : null
                })
                .ToListAsync();

            return Ok(new
            {
                pagination.PageNumber,
                pagination.PageSize,
                Total = total,
                Data = characters
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> GetById(int id)
        {
            var c = await _context.Characters
                .Include(c => c.House)
                .Include(c => c.NovaCasa)
                .Include(c => c.Fortaleza)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (c == null)
                return NotFound();

            return new CharacterDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Titulo = c.Titulo,
                Status = c.Status,
                Descricao = c.Descricao,
                Sexo = c.Sexo,
                Casa = c.House?.Nome,
                NovaCasa = c.NovaCasa?.Nome,
                Fortaleza = c.Fortaleza?.Nome
            };
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CharacterDto>> Create(CharacterDto dto)
        {
            var character = new Character
            {
                Nome = dto.Nome,
                Titulo = dto.Titulo,
                Status = dto.Status,
                Descricao = dto.Descricao,
                Sexo = dto.Sexo,
                HouseId = _context.Houses.FirstOrDefault(h => h.Nome == dto.Casa)?.Id,
                NovaCasaId = _context.Houses.FirstOrDefault(h => h.Nome == dto.NovaCasa)?.Id,
                FortalezaId = _context.Strongholds.FirstOrDefault(f => f.Nome == dto.Fortaleza)?.Id
            };

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            dto.Id = character.Id;

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CharacterDto dto)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return NotFound();

            character.Nome = dto.Nome;
            character.Titulo = dto.Titulo;
            character.Status = dto.Status;
            character.Descricao = dto.Descricao;
            character.Sexo = dto.Sexo;
            character.HouseId = _context.Houses.FirstOrDefault(h => h.Nome == dto.Casa)?.Id;
            character.NovaCasaId = _context.Houses.FirstOrDefault(h => h.Nome == dto.NovaCasa)?.Id;
            character.FortalezaId = _context.Strongholds.FirstOrDefault(f => f.Nome == dto.Fortaleza)?.Id;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return NotFound();

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
