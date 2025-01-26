using BoardGameStore.Data;
using BoardGameStore.Models;
using BoardGameStore.Models.Enums;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int boardGameId)
        {
            var boardGame =  _context.BoardGames
                 .FirstOrDefault(b => b.BoardGameId == boardGameId);

            return View(boardGame);
        }
        [HttpPost]
        public async Task<IActionResult> Submit(int boardGameId, int quantity)
        {
            var boardGame = _context.BoardGames
                 .FirstOrDefault(b => b.BoardGameId == boardGameId);
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var purchase = new Purchase
            {
                BoardGameId = boardGameId,
                PurchaseDate = DateTime.UtcNow,
                PurchaseQuantity = quantity,
                UserId = userId,
                Total = quantity * boardGame.PurchasePrice
            };
            boardGame.Quantity -= quantity;
            if (boardGame.Quantity<=0)
            {
                boardGame.Status = Status.Unavailable;
            }
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
            return RedirectToAction(nameof(Confirm),purchase);
        }
        [HttpGet]
        public async Task<IActionResult> Confirm(Purchase purchase)
        {
            var purchaseView = _context.Purchases
                .Include(b => b.BoardGame)
                .FirstOrDefault(p => p.PurchaseId == purchase.PurchaseId);
            return View(purchaseView);
        }
    }
}
