
using HCL1_Demo_Web_Application.Data;
using HCL1_Demo_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HCL1_Demo_Web_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FoodItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetFoodItems(
            string? category, bool availableOnly = false)
        {
            var query = _context.FoodItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(f => f.Category == category);

            if (availableOnly)
                query = query.Where(f => f.IsAvailable);

            return Ok(await query.OrderBy(f => f.Id).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FoodItem>> GetFoodItem(int id)
        {
            var item = await _context.FoodItems.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<FoodItem>> CreateFoodItem(FoodItem item)
        {
            _context.FoodItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodItem),
                new { id = item.Id }, item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateFoodItem(int id, FoodItem item)
        {
            if (id != item.Id)
                return BadRequest("URL ID and body ID must match.");

            var existing = await _context.FoodItems.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = item.Name;
            existing.Category = item.Category;
            existing.Description = item.Description;
            existing.Price = item.Price;
            existing.IsAvailable = item.IsAvailable;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var item = await _context.FoodItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.FoodItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
