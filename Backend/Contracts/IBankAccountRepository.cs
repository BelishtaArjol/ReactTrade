using Entities.Helpers;
using Entities.Models;
using Entities.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetBankAccountsAsync(PagingParameter pagingParameter);
        Task<BankAccount> GetBankAccountByIdAsync(int id);
        Task<BankAccount> GetBankAccountByCodeAsync(string Code, int id);
        void CreateBankAccount(BankAccount bankAccount);
        void UpdateBankAccount(BankAccount bankAccount);
        void DeleteBankAccount(BankAccount bankAccount);
        Task<PaginationHelper<IEnumerable<BankAccount>>> GetAllBankAccountsAsync(PagingParameter pagingParameter);
        Task<PaginationHelper<IEnumerable<BankAccount>>> GetAllBankAccountsClientsAsync(PagingParameter pagingParameter, int clientId);

    }
}
