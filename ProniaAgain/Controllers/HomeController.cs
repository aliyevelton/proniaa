using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAgain.Contexts;
using ProniaP336.ViewModels;

namespace ProniaAgain.Controllers
{
    public class HomeController : Controller
    {
        public readonly ProniaDbContext _context;
        public HomeController(ProniaDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var shipping = await _context.Shippings.ToListAsync();
            var slider = await _context.Sliders.ToListAsync();
            var model = new HomeViewModel
            {
                Sliders = slider,
                Shippings = shipping
            };
            return View(model);
        }
    }
}
