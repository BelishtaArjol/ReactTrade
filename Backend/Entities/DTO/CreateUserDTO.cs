using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Fist name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime Birthdate { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
