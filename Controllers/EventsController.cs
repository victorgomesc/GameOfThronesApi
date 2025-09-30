using GameOfThronesAPI.Data;
using GameOfThronesAPI.DTOs;
using GameOfThronesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameOfThronesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
        {
            var events = await _context.Events
                .Include(e => e.Characters)
                .Include(e => e.Houses)
                .Include(e => e.Stronghold)
                .ToListAsync();

            return events.Select(e => new EventDto
            {
                Id = e.Id,
                Nome = e.Nome,
                Descricao = e.Descricao,
                Data = e.Data,
                Characters = e.Characters.Select(c => c.Nome).ToList(),
                Houses = e.Houses.Select(h => h.Nome).ToList(),
                Stronghold = e.Stronghold?.Nome
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {
            var e = await _context.Events
                .Include(ev => ev.Characters)
                .Include(ev => ev.Houses)
                .Include(ev => ev.Stronghold)
                .FirstOrDefaultAsync(ev => ev.Id == id);

            if (e == null) return NotFound();

            return new EventDto
            {
                Id = e.Id,
                Nome = e.Nome,
                Descricao = e.Descricao,
                Data = e.Data,
                Characters = e.Characters.Select(c => c.Nome).ToList(),
                Houses = e.Houses.Select(h => h.Nome).ToList(),
                Stronghold = e.Stronghold?.Nome
            };
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Event>> Create(Event e)
        {
            _context.Events.Add(e);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = e.Id }, e);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Event e)
        {
            if (id != e.Id) return BadRequest();

            _context.Entry(e).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.Events.FindAsync(id);
            if (e == null) return NotFound();

            _context.Events.Remove(e);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
