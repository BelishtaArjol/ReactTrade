namespace Entities.Dto
{
    public class TransactionByAccountIdDTO
    {
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
    }
}
