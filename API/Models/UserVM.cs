using API.Data;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserVM
    {
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(15, ErrorMessage = "Name cannot be longer than 15 characters")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Name cannot contain whitespace")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter fullname")]
        [MaxLength(100, ErrorMessage = "Fullname cannot be longer than 100 characters")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Please enter phone")]
        [MaxLength(20, ErrorMessage = "Phone number cannot be longer than 20 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        [MaxLength(255, ErrorMessage = "Address cannot be longer than 255 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter role")]
        [MaxLength(50, ErrorMessage = "Role cannot be longer than 50 characters")]  // Assuming a maximum length for role
        public string Role { get; set; }


    }
}
