using Action = Entities.Models.Action;

namespace Entities.Dto
{
    public class BankTransactionDTO
    {

        public int BankAccountId { get; set; }
        public Action Action { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
