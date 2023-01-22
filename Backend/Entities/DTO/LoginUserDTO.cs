using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}