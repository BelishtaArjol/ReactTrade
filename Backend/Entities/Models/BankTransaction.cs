using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public enum Action
    {
        Deposit,
        Withdraw
    }

    public class BankTransaction
    {
        [Key]
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public Action Action { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        [ForeignKey("BankAccountId")]
        public BankAccount BankAccount { get; set; }
    }
}
