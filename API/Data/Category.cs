using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string CategoryName { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
