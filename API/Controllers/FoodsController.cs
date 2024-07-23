using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Repository.Foods;
using API.Models;
using NuGet.Protocol.Core.Types;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IrepositoryFood repository;

        public FoodsController(IrepositoryFood repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IEnumerable<Food> GetFoods(string? sort, string? search, string? pireRange, string? category)
        {
            return repository.GetFoods(sort, search, pireRange,category);
        }
        // GET: api/Foods/{id}
        [HttpGet("{id}")]
        public ActionResult<Food> GetFoodById(int id)
        {
            var food = repository.GetFoodById(id);
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }
    }
}
