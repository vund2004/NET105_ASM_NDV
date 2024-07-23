using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CategoryVM
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string CategoryName { get; set; }

    }
}
