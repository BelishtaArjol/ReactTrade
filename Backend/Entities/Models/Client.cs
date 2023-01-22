using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        [Required]
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
