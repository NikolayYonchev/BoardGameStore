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
using Microsoft.AspNetCore.Authorization;

namespace BoardGameStore.Controllers
{
	public class RentalsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public RentalsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Index(int boardGameId)
		{
			var boardGame = await _context.BoardGames
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

		[HttpPost]
		public async Task<IActionResult> Submit(RentalInputModel rentalInputModel)
		{
			var boardGame = await _context.BoardGames
				 .FirstOrDefaultAsync(b => b.BoardGameId == rentalInputModel.BoardGameId);
			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			var rental = new Rental()
			{
				BoardGameId = rentalInputModel.BoardGameId,
				UserId = userId,
				RentalDate = DateTime.UtcNow,
				ReturnDate = rentalInputModel.RentalEndDate,
				Total = (rentalInputModel.RentalEndDate.Day- DateTime.UtcNow.Day)*boardGame.RentalPricePerDay
			};
			boardGame.Quantity--;
			if (boardGame.Quantity<=0)
			{
				boardGame.Status = Status.Unavailable;
			}
			_context.Rentals.Add(rental);
			_context.SaveChanges();
			
			var rentalOutput = new RentalOutputModel
			{
				BoardGameId = rental.BoardGameId ,
				Total = rental.Total,
				UserId= userId,
				ReturnDate= rental.ReturnDate,
				Title = boardGame.Title
			};
			return RedirectToAction(nameof(Confirm),rentalOutput);
		}
		[HttpGet]
		public async Task<IActionResult> Confirm(RentalOutputModel outputModel)
		{
			return View(outputModel);
		}
	}
}
