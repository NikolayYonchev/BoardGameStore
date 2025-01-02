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

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals.Include(r => r.BoardGame).Include(r => r.User).ToListAsync();
            return View(rentals);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                // Validate that the return date is after the rental date
                if (rental.ReturnDate <= rental.RentalDate)
                {
                    ModelState.AddModelError("ReturnDate", "Return date must be after the rental date.");
                    return View(rental);
                }

                rental.Status = Status.Available; // Set initial status to "Active"
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rental);
        }

        // POST: Rentals/Return
        [HttpPost]
        public async Task<IActionResult> Return(int rentalId)
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

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.BoardGame)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }
    }
}
