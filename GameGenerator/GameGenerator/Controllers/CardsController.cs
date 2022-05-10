using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardGenerator.Extensions;
using GameGenerator.Models;
using GameGenerator.Core.Exceptions;
using GameGenerator.Core.Abstractions.Services;

namespace CardGenerator.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
            var cardEntries = await _cardService.GetAllAsync();
            var viewModels = cardEntries.Select(c => c.ToViewModel()).ToList();
            return View(viewModels);
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardEntry = await _cardService.GetByIdAsync(id.Value);

            if (cardEntry == null)
            {
                return NotFound();
            }

            return View(cardEntry.ToViewModel());
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CardEntryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _cardService.CreateAsync(viewModel.ToCardEntry());
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardEntry = await _cardService.GetByIdAsync(id.Value);
            if (cardEntry == null)
            {
                return NotFound();
            }
            return View(cardEntry.ToViewModel());
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CardEntryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cardService.UpdateAsync(id, viewModel.ToCardEntry());

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

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardEntry = await _cardService.GetByIdAsync(id.Value);

            if (cardEntry == null)
            {
                return NotFound();
            }

            return View(cardEntry.ToViewModel());
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _cardService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
