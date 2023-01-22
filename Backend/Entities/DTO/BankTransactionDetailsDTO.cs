namespace Entities.Dto
{
    public class BankTransactionDetailsDTO
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public Models.Action Action { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
