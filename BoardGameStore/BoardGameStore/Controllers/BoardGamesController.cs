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

namespace BoardGameStore.Controllers
{
    public class BoardGamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardGamesController(ApplicationDbContext context)
        {
            _context = context;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardGame boardGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boardGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boardGame);
        }

        // similar to details action

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
        
        // do not use magic strings
        // logic should be inside controller

        // POST: BoardGames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BoardGameId,Title,Category,MinPlayers,MaxPlayers,Description,RentalPricePerDay,PurchasePrice,Quantity,Condition")] BoardGame boardGame)
        {
            if (id != boardGame.BoardGameId)
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
            return View(boardGame);
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
