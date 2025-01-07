using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardGameStore.Models;
using BoardGameStore.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using BoardGameStore.Models.Enums;
using BoardGameStore.ViewModels;

namespace BoardGameStore.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Index(int boardGameId)
        {
            
            var boardGame = await _context.BoardGames
                 //.Include(x => x.BoardGame)
                 .FirstOrDefaultAsync(b => b.BoardGameId == boardGameId);
            
            var rentalViewModel = new RentalViewModel
            {
                BoardGameId = boardGame.BoardGameId,
                Title = boardGame.Title,
                ImageUrl = boardGame.ImageUrl,
                Condition = boardGame.Condition,
                Description = boardGame.Description,
                RentalPricePerDay = boardGame.RentalPricePerDay
            };

            return View(rentalViewModel);
        }

        // POST: Rentals/Return
        [HttpPost]
        public async Task<IActionResult> Submit(RentalInputModel model)
        {
            _context.Add(new Rental
            {
                BoardGameId = model.BoardGameId,
                UserId = this.User.Identity.Name,
                RentalDate = DateTime.Now,
                Status = Status.Available
            });
            var boardGame = await _context.BoardGames.FindAsync(model.BoardGameId);
            var rental = new Rental()
            {
                BoardGameId = model.BoardGameId,
                UserId = this.User.Identity.Name,
                RentalDate = DateTime.Now,
                Status = Status.Available
            };
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            if (rental.Status != Status.Available)
            {
                return BadRequest("This rental is not currently Available.");
            }

            rental.ReturnDate = DateTime.Now;
            rental.Status = Status.Returned;

            _context.Update(rental);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
