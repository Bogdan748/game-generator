using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameGenerator.Models;
using GameGenerator.Core.Services;
using GameGenerator.Extensions;
using GameGenerator.Core.Exceptions;

namespace GameGenerator.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var gameEntries = await _gameService.GetAllAsync();
            var viewModels = gameEntries.Select(c => c.ToViewModel()).ToList();
            return View(viewModels);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameEntry = await _gameService.GetByIdAsync(id.Value);
                
            if (gameEntry == null)
            {
                return NotFound();
            }

            return View(gameEntry.ToViewModel());
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GameEntryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _gameService.CreateAsync(viewModel.ToGameEntry());
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameEntry = await _gameService.GetByIdAsync(id.Value);
            if (gameEntry == null)
            {
                return NotFound();
            }
            return View(gameEntry.ToViewModel());
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GameEntryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gameService.UpdateAsync(id, viewModel.ToGameEntry());

                }
                catch (EntryDoesNotExistException)
                {
                    return NotFound();
                }
                catch (EntryUpdateErrorException)
                {
                    throw;
                }


                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameEntry = await _gameService.GetByIdAsync(id.Value);
                
            if (gameEntry == null)
            {
                return NotFound();
            }

            return View(gameEntry.ToViewModel());
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _gameService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
