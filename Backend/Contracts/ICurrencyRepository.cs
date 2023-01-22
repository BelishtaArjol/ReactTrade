using Entities.Dto;
using Entities.Models;
using Entities.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICurrencyRepository
    {
        Task<Currency> GetCurrencyByIdAsync(int id);
        Task<Currency> GetCurrencyByCodeAsync(string code);
        void CreateCurrency(Currency currency);
        void UpdateCurrency(Currency currency);
        void DeleteCurrency(Currency currency);
        Task<PaginationDTO<IEnumerable<Currency>>> GetAllAsync(PagingParameter pagingParameter);
    }
}
