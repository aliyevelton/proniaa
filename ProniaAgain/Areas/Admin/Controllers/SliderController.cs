using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAgain.Areas.Admin.Helpers;
using ProniaAgain.Areas.Admin.ViewModels;
using ProniaAgain.Contexts;
using ProniaAgain.Models;

namespace ProniaP336.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderController : Controller
{
    private readonly ProniaDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public SliderController(ProniaDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.ToListAsync();
        return View(sliders);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SliderCreateViewModel slider)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }

        if(!slider.Image.IsSize(4000))
        {
            ModelState.AddModelError("Image", "Shekil olcusu 4mb-dan cox ola bilmez");
            return View();
        }

        if (!slider.Image.IsImage("image/"))
        {
            ModelState.AddModelError("Image", "Shekil secin");
            return View();
        }

        string fileName = $"{Guid.NewGuid()}-{slider.Image.FileName}";
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", 
          fileName);
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            await slider.Image.CopyToAsync(stream);

        }
        Slider newSlider = new Slider
        {
            Offer = slider.Offer,
            Title = slider.Title,
            Description = slider.Description,
            Image = fileName
        };
        await _context.Sliders.AddAsync(newSlider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var slide = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slide == null)
        {
            return NotFound();
        }
        return View(slide);
    }
    [HttpPost]
    [ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteSlide(int id)
    {
        var slide = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slide == null)
        {
            return NotFound();
        }

        _context.Sliders.Remove(slide);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Detail(int id)
    {
        var slide = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slide == null)
        {
            return NotFound();
        }
        return View(slide);
    }
}

