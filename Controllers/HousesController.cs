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
    public class HousesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HousesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseDto>>> GetAll()
        {
            var houses = await _context.Houses
                .Include(h => h.Stronghold)
                .Include(h => h.Vassalos)
                .ToListAsync();

            var result = houses.Select(h => new HouseDto
            {
                Id = h.Id,
                Nome = h.Nome,
                Idade = h.Idade,
                Lema = h.Lema,
                Fortaleza = h.Stronghold != null ? h.Stronghold.Nome : null,
                Vassalos = h.Vassalos.Select(v => v.Nome).ToList()
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HouseDto>> GetById(int id)
        {
            var house = await _context.Houses
                .Include(h => h.Stronghold)
                .Include(h => h.Vassalos)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (house == null)
                return NotFound();

            var dto = new HouseDto
            {
                Id = house.Id,
                Nome = house.Nome,
                Idade = house.Idade,
                Lema = house.Lema,
                Fortaleza = house.Stronghold?.Nome,
                Vassalos = house.Vassalos.Select(v => v.Nome).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<House>> Create(House house)
        {
            _context.Houses.Add(house);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = house.Id }, house);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, House house)
        {
            if (id != house.Id)
                return BadRequest();

            _context.Entry(house).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var house = await _context.Houses.FindAsync(id);
            if (house == null)
                return NotFound();

            _context.Houses.Remove(house);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
