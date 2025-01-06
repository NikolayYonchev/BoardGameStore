using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardGameStore.Models;
using BoardGameStore.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using BoardGameStore.Models.Enums;

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
            /*if (boardGameId == null)
            {
                return NotFound();
            }*/
            //trqbva rental da vrushta rental no trqbva da zareda board game
            var rental = await _context.Rentals
                .Include(x => x.BoardGame)
                .FirstOrDefaultAsync(b=>b.BoardGame.BoardGameId == boardGameId);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Return
        [HttpPost]
        public async Task<IActionResult> Submit(int rentalId, DateTime rentalDate, DateTime returnDate)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental == null)
            {
                return NotFound();
            }

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
