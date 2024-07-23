using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Foods
{
    public class RepositoryFood : IrepositoryFood
    {
        private readonly CompanyContext _context;

        public RepositoryFood(CompanyContext context)
        {
            _context = context;
        }

        public Food GetFoodById(int id)
        {
            var Food = _context.Foods.Find(id);
            return Food;
        }

        public List<Food> GetFoods(string? sort, string? search, string? pireRange, string? category)
        {
            IQueryable<Food> query = _context.Foods;

            // Filter by name
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            // Filter by category
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category.CategoryName == category);
            }

            // Filter by price range
            if (!string.IsNullOrEmpty(pireRange))
            {
                switch (pireRange)
                {
                    case "0-20k":
                        query = query.Where(p => p.Price >= 0 && p.Price <= 20000);
                        break;
                    case "20-50k":
                        query = query.Where(p => p.Price >= 20000 && p.Price <= 50000);
                        break;
                    case "50k+":
                        query = query.Where(p => p.Price > 50000);
                        break;
                    default:
                        break;
                }
            }

            // Sort products
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "name_asc":
                        query = query.OrderBy(x => x.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case "price_asc":
                        query = query.OrderBy(x => x.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(x => x.Price);
                        break;
                    default:
                        query = query.OrderBy(x => x.FoodId);
                        break;
                }
            }

            // Select the data to return
            var foodVM = query.Select(p => new Food
            {
                FoodId = p.FoodId,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image, // Ensure only the filename is returned
                CreatedAt = p.CreatedAt,
                CategoryId = p.CategoryId
            }).ToList();

            return foodVM;
        }
      




    }
}
