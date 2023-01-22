using Contracts;
using Entities;
using Entities.Dto;
using Entities.Models;
using Entities.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCurrency(Currency currency) => Create(currency);

        public void UpdateCurrency(Currency currency) => Update(currency);

        public void DeleteCurrency(Currency currency) => Delete(currency);

        public async Task<Currency> GetCurrencyByIdAsync(int id) =>
            await FindByCondition(x => x.Id.Equals(id))
            .SingleOrDefaultAsync();

        public async Task<Currency> GetCurrencyByCodeAsync(string Code) =>
            await FindByCondition(x => x.Code.Equals(Code))
            .SingleOrDefaultAsync();

        public async Task<PaginationDTO<IEnumerable<Currency>>> GetAllAsync(PagingParameter pagingParameter)
        {
            var allResult = FindAll();
            var filteredData = await allResult
                .OrderBy(x => x.Code)
                .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
                .Take(pagingParameter.PageSize)
                .ToListAsync();

            var result = new PaginationDTO<IEnumerable<Currency>>
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
