namespace Entities.Dto
{
    public class BankAccountDetailsDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
        public bool IsActive { get; set; }

    }
}
