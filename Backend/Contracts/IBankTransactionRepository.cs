using Entities.Dto;
using Entities.Helpers;
using Entities.Models;
using Entities.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBankTransactionRepository
    {
        Task<IEnumerable<BankTransaction>> GetBankTransactionsAsync(PagingParameter pagingParameter);
        Task<BankTransaction> GetBankTransactionByIdAsync(int id);
        void CreateBankTransaction(BankTransaction transaction);
        void UpdateBankTransaction(BankTransaction transaction);
        void DeleteBankTransaction(BankTransaction transaction);
        Task<PaginationDTO<IEnumerable<BankTransaction>>> GetAllBankTransactionsAsync(PagingParameter pagingParameter);
        Task<PaginationHelper<IEnumerable<BankTransaction>>> GetAllBankAccountsTransactionAsync(PagingParameter pagingParameter, int bankAcccountId, int clientId);

    }
}
