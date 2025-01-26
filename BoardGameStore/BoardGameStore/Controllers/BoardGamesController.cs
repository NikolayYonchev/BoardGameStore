using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoardGameStore.Data;
using BoardGameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace BoardGameStore.Controllers
{
    public class BoardGamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BoardGamesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: BoardGames
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.BoardGames.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGame = await _context.BoardGames
                .FirstOrDefaultAsync(m => m.BoardGameId == id);
            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        public IActionResult Create()
        {
            return View();
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardGame boardGame)
        {
            if (boardGame.Image != null)
            {
                CheckPath(boardGame);
            }
            if (ModelState.IsValid)
            {
                _context.Add(boardGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(boardGame);
        }


        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var boardGame = await _context.BoardGames.FindAsync(id);
            if (boardGame == null)
            {
                return NotFound();
            }
            return View(boardGame);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BoardGame boardGame)
        {
            if (id != boardGame.BoardGameId)
            {
                return NotFound();
            }

            var existingBoardGame = await _context.BoardGames.AsNoTracking()
                .FirstOrDefaultAsync(b => b.BoardGameId == id);
            if (existingBoardGame == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    boardGame.ImageUrl = existingBoardGame.ImageUrl;
                    _context.Update(boardGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.BoardGames.Any(b => b.BoardGameId == boardGame.BoardGameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index),"Home");
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var boardGame = await _context.BoardGames
                .FirstOrDefaultAsync(m => m.BoardGameId == id);

            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boardGame = await _context.BoardGames.FindAsync(id);
            if (boardGame != null)
            {
                _context.BoardGames.Remove(boardGame);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardGameExists(int id)
        {
            return _context.BoardGames.Any(e => e.BoardGameId == id);
        }
        private void CheckPath(BoardGame boardGame)
        {

            // Generate the path to save the file
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

            // Create a unique file name to avoid conflicts
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(boardGame.Image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                boardGame.Image.CopyTo(fileStream);
            }

            // Set the ImageUrl to the relative path
            boardGame.ImageUrl = "/images/" + uniqueFileName;

        }
    }
}
