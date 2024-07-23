using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class FoodsController : Controller
    {
        private readonly HttpClient _httpClient;

        public FoodsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44325/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? sort, string? search, string? pireRange, string? category)
        {
            List<Food> foods = new List<Food>();

            string requestUri = $"Foods/GetFoods?sort={sort}&search={search}&pireRange={pireRange}&category={category}";
            HttpResponseMessage resMessage = await _httpClient.GetAsync(requestUri);

            if (resMessage.IsSuccessStatusCode)
            {
                string data = await resMessage.Content.ReadAsStringAsync();
                foods = JsonConvert.DeserializeObject<List<Food>>(data);
            }

            return View(foods);
        }
        [HttpGet]
        public async Task<IActionResult> FoodDetail(int id)
        {
            Food food = null;
            string requestUri = $"Foods/GetFoodById/{id}";
            HttpResponseMessage resMessage = await _httpClient.GetAsync(requestUri);

            if (resMessage.IsSuccessStatusCode)
            {
                string data = await resMessage.Content.ReadAsStringAsync();
                food = JsonConvert.DeserializeObject<Food>(data);
            }

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }
    }
}
