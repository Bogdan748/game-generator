
using GameGenerator.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameGenerator.Controllers
{
    public class CreatorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreatorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.CardEntity.ToListAsync());
        }
    }
}
