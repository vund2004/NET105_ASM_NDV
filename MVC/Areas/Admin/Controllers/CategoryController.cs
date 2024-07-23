using API.Data;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET104_ASM.Models.Authentication;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Controllers
{
    [Authentication]

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly CompanyContext _context;
        private readonly IWebHostEnvironment _environment;

        public CategoryController(CompanyContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.OrderByDescending(c => c.CategoryId).ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            var category = new Category
            {
                CategoryName = categoryVM.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var categoryVM = new CategoryVM
            {
                CategoryName = category.CategoryName
            };
            ViewData["CategoryId"] = category.CategoryId;
            return View(categoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryVM categoryVM)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = category.CategoryId;
                return View(categoryVM);
            }

            category.CategoryName = categoryVM.CategoryName;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                // Optionally log that the category was not found
                return RedirectToAction("Index");
            }

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally handle the error (e.g., return an error view or message)
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Optional: Check if there are any foods associated with this category and handle accordingly.
            // var foods = await _context.Foods.Where(f => f.CategoryId == id).ToListAsync();
            // if (foods.Any())
            // {
            //     // Handle associated foods (e.g., delete, reassign, etc.)
            // }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Foods)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
    }
}
