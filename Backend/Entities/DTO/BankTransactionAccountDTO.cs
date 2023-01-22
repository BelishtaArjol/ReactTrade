using System;

namespace Entities.Dto
{
    public class BankTransactionAccountDTO
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
