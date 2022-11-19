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
        public async Task<IActionResult> Index()
        {
            var filters = await _database.Filters.ToListAsync();
            return View(filters);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] Filter filter)
        {
            await _database.AddAsync(filter);
            await _database.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}