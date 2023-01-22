using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class UpdateClientDTO
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime Birthdate { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
    }
}
