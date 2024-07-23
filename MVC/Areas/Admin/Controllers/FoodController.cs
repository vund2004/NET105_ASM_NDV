using API.Data;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NET104_ASM.Models.Authentication;
using System;
using System.IO; // Add this for Path
using System.Linq;

namespace MVC.Areas.Admin.Controllers
{
    [Authentication]

    [Area("Admin")]
    public class FoodController : Controller
    {
        private readonly CompanyContext context;
        private readonly IWebHostEnvironment environment; // Corrected variable name

        public FoodController(CompanyContext context, IWebHostEnvironment environment)
        {
            this.context = context; // Corrected assignment
            this.environment = environment; // Corrected assignment
        }

        public IActionResult Index()
        {
            // Truy xuất các mục thực phẩm cùng với thông tin danh mục
            var food = context.Foods.Include(f => f.Category).OrderByDescending(f => f.FoodId).ToList();
            return View(food);
        }


        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "CategoryName"); // Corrected access to _context
            return View();
        }

        [HttpPost]
        public IActionResult Create(FoodVM foodVM)
        {
            ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "CategoryName"); // Corrected access to _context

            if (foodVM.Image == null)
            {
                ModelState.AddModelError("Image", "*Ảnh sản phẩm không được bỏ trống");
            }
            if (!ModelState.IsValid)
            {
                return View(foodVM);
            }

            string newFileName = Path.GetFileName(foodVM.Image!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName; // Corrected usage of Path.Combine
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                foodVM.Image.CopyTo(stream);
            }

            Food food = new Food()
            {
                Name = foodVM.Name,
                Price = foodVM.Price,
                Image = newFileName,
                Available = foodVM.Available,
                CreatedAt = DateTime.Now,
                CategoryId = foodVM.CategoryId,
            };

            context.Foods.Add(food);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "CategoryName");

            var food = context.Foods.Find(id);
            if (food == null)
            {
                return RedirectToAction("Index");
            }

            var foodVM = new FoodVM()
            {
                Name = food.Name,
                Price = food.Price,
                Available = food.Available,
                CreatedAt = food.CreatedAt, // Include CreatedAt
                CategoryId = food.CategoryId,
            };

            ViewData["FoodId"] = food.FoodId;
            ViewData["image"] = food.Image;
            return View(foodVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, FoodVM foodVM)
        {
            var food = context.Foods.Find(id);
            if (food == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "CategoryName");
                ViewData["FoodId"] = food.FoodId;
                ViewData["image"] = food.Image;
                ViewData["CreatedAt"] = food.CreatedAt;
                return View(foodVM);
            }

            string newFileName = food.Image;

            if (foodVM.Image != null)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(foodVM.Image.FileName);
                string imageFullPath = Path.Combine(environment.WebRootPath, "images", newFileName);

                try
                {
                    using (var stream = new FileStream(imageFullPath, FileMode.Create))
                    {
                        foodVM.Image.CopyTo(stream);
                    }

                    string oldImageFullPath = Path.Combine(environment.WebRootPath, "images", food.Image);
                    if (System.IO.File.Exists(oldImageFullPath))
                    {
                        System.IO.File.Delete(oldImageFullPath);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while uploading the image.");
                    ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "CategoryName");
                    ViewData["FoodId"] = food.FoodId;
                    ViewData["image"] = food.Image;
                    ViewData["CreatedAt"] = food.CreatedAt;
                    return View(foodVM);
                }
            }

            food.Name = foodVM.Name;
            food.Price = foodVM.Price;
            food.Image = newFileName;
            food.Available = foodVM.Available;
            food.CategoryId = foodVM.CategoryId; // Update the CategoryId
            if (foodVM.CreatedAt != default(DateTime))
            {
                food.CreatedAt = foodVM.CreatedAt;
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var food = context.Foods.Find(id);
            if (food == null)
            {
                return RedirectToAction("Index");
            }

            string imageFullPath = environment.WebRootPath + "/images/" + food.Image;
            System.IO.File.Delete(imageFullPath);
            context.Foods.Remove(food);
            context.SaveChanges(true);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var food = context.Foods.Include(f => f.Category).FirstOrDefault(f => f.FoodId == id);
            if (food == null)
            {
                return RedirectToAction("Index");
            }

            return View(food);
        }

    }
}
