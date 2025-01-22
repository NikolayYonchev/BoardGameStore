using BoardGameStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int boardGameId)
        {
            var boardGame =  _context.BoardGames
                 .FirstOrDefault(b => b.BoardGameId == boardGameId);

            return View(boardGame);
        }
    }
}
