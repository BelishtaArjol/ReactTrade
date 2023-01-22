using Contracts;
using Entities;
using Entities.Dto;
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
    public class BankTransactionRepository : RepositoryBase<BankTransaction>, IBankTransactionRepository
    {
        public BankTransactionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateBankTransaction(BankTransaction transaction) => Create(transaction);

        public void DeleteBankTransaction(BankTransaction transaction) => Delete(transaction);

        public async Task<BankTransaction> GetBankTransactionByIdAsync(int id) =>
            await FindByCondition(x => x.Id.Equals(id) && x.IsActive == true)
            .SingleOrDefaultAsync();


        public async Task<IEnumerable<BankTransaction>> GetBankTransactionsAsync(PagingParameter pagingParameter) =>
            await FindByCondition(x => x.IsActive == true)
            .OrderByDescending(x => x.DateCreated)
            .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
            .Take(pagingParameter.PageSize)
            .ToListAsync();

        public void UpdateBankTransaction(BankTransaction transaction) => Update(transaction);

        public async Task<PaginationDTO<IEnumerable<BankTransaction>>> GetAllBankTransactionsAsync(PagingParameter pagingParameter)
        {
            var allResult = FindByCondition(x => x.IsActive == true);
            var filteredData = await allResult
                .OrderByDescending(x => x.DateCreated)
                .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
                .Take(pagingParameter.PageSize)
                .ToListAsync();

            var result = new PaginationDTO<IEnumerable<BankTransaction>>
            {
                Data = filteredData,
                CurrentPage = pagingParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(allResult.Count() / (double)pagingParameter.PageSize),
                PageSize = pagingParameter.PageSize,
                TotalCount = allResult.Count()
            };

            return result;
        }

        public async Task<PaginationHelper<IEnumerable<BankTransaction>>> GetAllBankAccountsTransactionAsync(PagingParameter pagingParameter, int bankAccountId, int clientId)
        {
            var allResult = FindByCondition(x => x.IsActive == true && x.BankAccountId == bankAccountId && x.BankAccount.Client.Id == clientId).Include(x => x.BankAccount).Include(y => y.BankAccount.Client);
            var filteredData = await allResult
                .OrderBy(x => x.DateCreated)
                .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
                .Take(pagingParameter.PageSize)
                .ToListAsync();

            var result = new PaginationHelper<IEnumerable<BankTransaction>>
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
