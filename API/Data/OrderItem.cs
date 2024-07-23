using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string NameOrder { get; set; }
        [Required]
        public string ImageOrder { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }
    }
}
