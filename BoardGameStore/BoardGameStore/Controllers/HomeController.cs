using System.Diagnostics;
using BoardGameStore.Data;
using BoardGameStore.Models;
using BoardGameStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var boardGames = _context.BoardGames.ToList();

            ViewBag.UserId = userId;

            return View(boardGames);
        }

        public IActionResult Search(int minPlayers, int maxPlayers) 
        {
            var boardGames = _context.BoardGames
                .Where(x=>x.MinPlayers == minPlayers && x.MaxPlayers == maxPlayers)
                .ToList();

            return View("Index", boardGames);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
