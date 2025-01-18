using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardGameStore.Models;
using BoardGameStore.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using BoardGameStore.Models.Enums;
using BoardGameStore.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

			var rentalViewModel = new RentalViewModel
			{
				BoardGameId = boardGame.BoardGameId,
				UserId = userId,
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
		public async Task<IActionResult> Submit(RentalInputModel rentalInputModel)
		{
			//TODO
			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			var rental = new Rental()
			{
				BoardGameId = rentalInputModel.BoardGameId,
				UserId = userId,
				RentalDate = DateTime.UtcNow,
				ReturnDate = rentalInputModel.RentalEndDate,
			};
			_context.Rentals.Add(rental);
			_context.SaveChanges();
			//if remember me btn is ticked when logging in the return will fail because the userId will be different
			return RedirectToAction(nameof(Confirm));
		}
		[HttpGet]
		public async Task<IActionResult> Confirm()
		{
			// rentalviewmodel must be returned
			return View();
		}
	}
}
