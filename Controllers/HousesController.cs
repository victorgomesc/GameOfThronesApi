using GameOfThronesAPI.Data;
using GameOfThronesAPI.DTOs;
using GameOfThronesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameOfThronesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HousesController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseDto>>> GetAll()
        {
            return await _context.Houses
                .Include(h => h.Stronghold)
                .Include(h => h.Vassalos)
                .Select(h => new HouseDto
                {
                    Id = h.Id,
                    Nome = h.Nome,
                    Idade = h.Idade,
                    Lema = h.Lema,
                    Fortaleza = h.Stronghold != null ? h.Stronghold.Nome : null,
                    Vassalos = h.Vassalos.Select(v => v.Nome).ToList()
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HouseDto>> GetById(int id)
        {
            var house = await _context.Houses
                .Include(h => h.Stronghold)
                .Include(h => h.Vassalos)
                .Where(h => h.Id == id)
                .Select(h => new HouseDto
                {
                    Id = h.Id,
                    Nome = h.Nome,
                    Idade = h.Idade,
                    Lema = h.Lema,
                    Fortaleza = h.Stronghold != null ? h.Stronghold.Nome : null,
                    Vassalos = h.Vassalos.Select(v => v.Nome).ToList()
                })
                .FirstOrDefaultAsync();

            if (house == null) return NotFound();
            return house;
        }


        [HttpPost]
        public async Task<ActionResult<House>> Create(House house)
        {
            _context.Houses.Add(house);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = house.Id }, house);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, House house)
        {
            if (id != house.Id) return BadRequest();

            _context.Entry(house).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var house = await _context.Houses.FindAsync(id);
            if (house == null) return NotFound();

            _context.Houses.Remove(house);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
