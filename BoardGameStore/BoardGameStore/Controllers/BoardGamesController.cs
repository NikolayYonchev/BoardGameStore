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
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.BoardGames.ToListAsync());
        }

        // id allows nullable, but checks if null afterwards => no need for the ? operator

        //logic inside controller => move to a service, check out dependency injection



        // GET: BoardGames/Details/5
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

        // GET: BoardGames/Create
        public IActionResult Create()
        {
            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardGame boardGame)
        {
            /*var image = boardGame.Image;
            var imageURl = boardGame.Image.ContentDisposition;
            var imageURl2 = boardGame.Image.ContentType;
            var imageURl3 = boardGame.Image.Name;
            var imageURl4 = boardGame.Image.FileName; // the-mind.png
            var imageUR5 = boardGame.Image.Headers;*/
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


        // GET: BoardGames/Edit/5
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



        // POST: BoardGames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BoardGame boardGame)
        {
            if (id != boardGame.BoardGameId)
            {
                return NotFound();
            }

            // Fetch the existing board game from the database
            var existingBoardGame = await _context.BoardGames.AsNoTracking().FirstOrDefaultAsync(b => b.BoardGameId == id);
            if (existingBoardGame == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            /*  if (id != boardGame.BoardGameId)
              {
                  return NotFound();
              }

              if (ModelState.IsValid)
              {
                  try
                  {
                      //CheckPath(boardGame);
                      _context.Update(boardGame);
                      await _context.SaveChangesAsync();
                  }
                  catch (DbUpdateConcurrencyException)
                  {
                      if (!BoardGameExists(boardGame.BoardGameId))
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
              return View(nameof(Index));*/
        }

        // GET: BoardGames/Delete/5\
        // use this -> [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // add delete
            var boardGame = await _context.BoardGames
                .FirstOrDefaultAsync(m => m.BoardGameId == id);

            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // POST: BoardGames/Delete/5
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
    }
}
