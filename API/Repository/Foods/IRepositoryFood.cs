using API.Data;
namespace API.Models
{
    public interface IrepositoryFood
    {
        List<Food> GetFoods(string? sort, string? search, string? pireRange, string? category);
        Food GetFoodById(int id);
    }
}
