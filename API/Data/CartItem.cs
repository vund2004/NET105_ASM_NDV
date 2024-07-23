using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required]
        public string NameCart { get; set; }
        [Required]
        public string ImageCart { get; set; }
        public decimal Price { get; set; }
    }
}
