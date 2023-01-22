using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class BankAccountDTO
    {

        public string Code { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
        public bool IsActive { get; set; }
    
    }
}