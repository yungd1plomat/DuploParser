using DuploParser.Data;
using DuploParser.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }
    }
}