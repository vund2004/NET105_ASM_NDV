using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class FoodVM
    {
        public int FoodId { get; set; } 
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
        public bool Available { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }

    }
}
