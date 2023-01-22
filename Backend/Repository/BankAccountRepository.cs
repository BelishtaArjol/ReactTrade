using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Entities.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Repository
{
    public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }


        public void CreateBankAccount(BankAccount bankAccount) => Create(bankAccount);

        public void DeleteBankAccount(BankAccount bankAccount) => Delete(bankAccount);

        public async Task<BankAccount> GetBankAccountByIdAsync(int id) =>
            await FindByCondition(x => x.Id.Equals(id) && x.IsActive == true)
            .SingleOrDefaultAsync();

        public async Task<BankAccount> GetBankAccountByCodeAsync(string code, int id) =>
            await FindByCondition(x => x.Code.Equals(code) && x.ClientId.Equals(id))
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync(PagingParameter pagingParameter) =>
            await FindByCondition(x => x.IsActive == true)
            .OrderBy(x => x.Code)
            .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
            .Take(pagingParameter.PageSize)
            .ToListAsync();

        public void UpdateBankAccount(BankAccount bankAccount) => Update(bankAccount);

        public async Task<PaginationHelper<IEnumerable<BankAccount>>> GetAllBankAccountsAsync(PagingParameter pagingParameter)
        {
            var allResult = FindByCondition(x => x.IsActive == true);
            var filteredData = await allResult
                .OrderBy(x => x.Name)
                .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
                .Take(pagingParameter.PageSize)
                .ToListAsync();

            var result = new PaginationHelper<IEnumerable<BankAccount>>
            {
                Data = filteredData,
                CurrentPage = pagingParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(allResult.Count() / (double)pagingParameter.PageSize),
                PageSize = pagingParameter.PageSize,
                TotalCount = allResult.Count()
            };

            return result;
        }

        public async Task<PaginationHelper<IEnumerable<BankAccount>>> GetAllBankAccountsClientsAsync(PagingParameter pagingParameter, int clientId)
        {
            var allResult = FindByCondition(x => x.IsActive == true && x.ClientId == clientId);
            var filteredData = await allResult
                .OrderBy(x => x.Name)
                .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
                .Take(pagingParameter.PageSize)
                .ToListAsync();

            var result = new PaginationHelper<IEnumerable<BankAccount>>
            {
                Data = filteredData,
                CurrentPage = pagingParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(allResult.Count() / (double)pagingParameter.PageSize),
                PageSize = pagingParameter.PageSize,
                TotalCount = allResult.Count()
            };

            return result;
        }
    }
}
