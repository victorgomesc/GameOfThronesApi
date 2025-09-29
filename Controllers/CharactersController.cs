using GameOfThronesAPI.Data;
using GameOfThronesAPI.DTOs;
using GameOfThronesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameOfThronesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAll()
        {
            return await _context.Characters
                .Include(c => c.House)
                .Include(c => c.NovaCasa)
                .Include(c => c.Fortaleza)
                .Select(c => new CharacterDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Titulo = c.Titulo,
                    Status = c.Status,
                    Sexo = c.Sexo,
                    Descricao = c.Descricao,
                    Casa = c.House != null ? c.House.Nome : null,
                    NovaCasa = c.NovaCasa != null ? c.NovaCasa.Nome : null,
                    Fortaleza = c.Fortaleza != null ? c.Fortaleza.Nome : null
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> GetById(int id)
        {
            var character = await _context.Characters
                .Include(c => c.House)
                .Include(c => c.NovaCasa)
                .Include(c => c.Fortaleza)
                .Where(c => c.Id == id)
                .Select(c => new CharacterDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Titulo = c.Titulo,
                    Status = c.Status,
                    Sexo = c.Sexo,
                    Descricao = c.Descricao,
                    Casa = c.House != null ? c.House.Nome : null,
                    NovaCasa = c.NovaCasa != null ? c.NovaCasa.Nome : null,
                    Fortaleza = c.Fortaleza != null ? c.Fortaleza.Nome : null
                })
                .FirstOrDefaultAsync();

            if (character == null) return NotFound();
            return character;
        }


        [HttpPost]
        public async Task<ActionResult<Character>> Create(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = character.Id }, character);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Character character)
        {
            character.Id = id; // força usar o id da rota
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
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
