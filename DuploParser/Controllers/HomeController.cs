using DuploParser.Data;
using DuploParser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DuploParser.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDb _database;

        public HomeController(ILogger<HomeController> logger, AppDb database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? code)
        {
            if (code is null || code != "d1plomat")
            {
                _logger.LogInformation("Unauthorized access attempt at Index");
                return Unauthorized("Соси хуй");
            }
            var filters = await _database.Filters.ToListAsync();
            return View(filters);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromQuery] string? code, [FromForm] Filter filter)
        {
            if (code is null || code != "d1plomat")
            {
                _logger.LogInformation("Unauthorized access attempt when adding");
                return Unauthorized("Соси хуй");
            }
            await _database.AddAsync(filter);
            await _database.SaveChangesAsync();
            return RedirectToAction("Index", new { code = "d1plomat" });
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(string? code, int id)
        {
            if (code is null || code != "d1plomat")
            {
                _logger.LogInformation("Unauthorized access attempt when deleting");
                return Unauthorized("Соси хуй");
            }
            var filter = await _database.Filters.FirstOrDefaultAsync(x => x.Id == id);
            if (filter is null)
                return NotFound();
            _database.Filters.Remove(filter);
            var count = _database.SaveChanges();
            _logger.LogInformation($"Deleted {count} filters");
            return RedirectToAction("Index", new { code = "d1plomat" });
        }
    }
}