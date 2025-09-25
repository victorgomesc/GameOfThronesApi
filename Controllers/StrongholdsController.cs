using GameOfThronesAPI.Data;
using GameOfThronesAPI.DTOs;
using GameOfThronesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Strongholds
                .Include(s => s.House)
                .Include(s => s.Residents)
                .Select(s => new StrongholdDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Localizacao = s.Localizacao,
                    Descricao = s.Descricao,
                    Casa = s.House != null ? s.House.Nome : null,
                    Residents = s.Residents.Select(r => r.Nome).ToList()
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StrongholdDto>> GetById(int id)
        {
            var stronghold = await _context.Strongholds
                .Include(s => s.House)
                .Include(s => s.Residents)
                .Where(s => s.Id == id)
                .Select(s => new StrongholdDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Localizacao = s.Localizacao,
                    Descricao = s.Descricao,
                    Casa = s.House != null ? s.House.Nome : null,
                    Residents = s.Residents.Select(r => r.Nome).ToList()
                })
                .FirstOrDefaultAsync();

            if (stronghold == null) return NotFound();
            return stronghold;
        }


        [HttpPost]
        public async Task<ActionResult<Stronghold>> Create(Stronghold stronghold)
        {
            _context.Strongholds.Add(stronghold);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stronghold.Id }, stronghold);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Stronghold stronghold)
        {
            if (id != stronghold.Id) return BadRequest();

            _context.Entry(stronghold).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
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
