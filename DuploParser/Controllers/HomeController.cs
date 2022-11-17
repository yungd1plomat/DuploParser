using DuploParser.Models;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DuploParser.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly LiteDatabase _database;

        public HomeController(ILogger<HomeController> logger, LiteDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}