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
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCategory(Category category) => Create(category);

        public void DeleteCategory(Category category) => Delete(category);

        public async Task<PaginationHelper<IEnumerable<Category>>> GetAllCategoryAsync(PagingParameter pagingParameter)
        {
            var allResult = FindAll();
            var filteredData = await allResult
                .OrderBy(x => x.Code)
                .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
                .Take(pagingParameter.PageSize)
                .ToListAsync();

            var result = new PaginationHelper<IEnumerable<Category>>
            {
                Data = filteredData,
                CurrentPage = pagingParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(allResult.Count() / (double)pagingParameter.PageSize),
                PageSize = pagingParameter.PageSize,
                TotalCount = allResult.Count()
            };

            return result;
        }

        public async Task<Category> GetCategoryByCodeAsync(string code) =>
       await FindByCondition(x => x.Code == code)
       .SingleOrDefaultAsync();

        public async Task<Category> GetCategoryByIdAsync(int id) =>
            await FindByCondition(x => x.Id.Equals(id))
            .SingleOrDefaultAsync();

        public void UpdateCategory(Category category) => Update(category);
    }
}
