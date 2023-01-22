namespace Entities.Dto
{
    public class CurrencyDetailDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
