using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
