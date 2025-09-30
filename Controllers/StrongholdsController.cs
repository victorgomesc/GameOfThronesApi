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
    public class StrongholdsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StrongholdDto>>> GetAll()
        {
            var strongholds = await _context.Strongholds
                .Include(s => s.House)
                .Include(s => s.Residents)
                .ToListAsync();

            return strongholds.Select(s => new StrongholdDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Localizacao = s.Localizacao,
                Descricao = s.Descricao,
                Casa = s.House?.Nome,
                Residents = s.Residents != null
                    ? s.Residents.Select(r => r.Nome).ToList()
                    : new List<string>()
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StrongholdDto>> GetById(int id)
        {
            var s = await _context.Strongholds
                .Include(s => s.House)
                .Include(s => s.Residents)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (s == null) return NotFound();

            return new StrongholdDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Localizacao = s.Localizacao,
                Descricao = s.Descricao,
                Casa = s.House?.Nome,
                Residents = s.Residents != null
                    ? s.Residents.Select(r => r.Nome).ToList()
                    : new List<string>()
            };
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StrongholdDto>> Create(StrongholdDto dto)
        {
            var stronghold = new Stronghold
            {
                Nome = dto.Nome,
                Localizacao = dto.Localizacao,
                Descricao = dto.Descricao,
                HouseId = _context.Houses.FirstOrDefault(h => h.Nome == dto.Casa)?.Id
            };

            _context.Strongholds.Add(stronghold);
            await _context.SaveChangesAsync();

            dto.Id = stronghold.Id;
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, StrongholdDto dto)
        {
            var stronghold = await _context.Strongholds.FindAsync(id);
            if (stronghold == null) return NotFound();

            stronghold.Nome = dto.Nome;
            stronghold.Localizacao = dto.Localizacao;
            stronghold.Descricao = dto.Descricao;
            stronghold.HouseId = _context.Houses.FirstOrDefault(h => h.Nome == dto.Casa)?.Id;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var stronghold = await _context.Strongholds.FindAsync(id);
            if (stronghold == null) return NotFound();

            _context.Strongholds.Remove(stronghold);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
