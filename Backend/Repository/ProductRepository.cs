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
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateProduct(Product product) => Create(product);

        public void DeleteProduct(Product product) => Delete(product);

        public async Task<Product> GetProductByIdAsync(int id) =>
            await FindByCondition(x => x.Id.Equals(id))
            .SingleOrDefaultAsync();

        public async Task<Product> FindCategoryforProductAsync(int categoryId) =>
           await FindByCondition(x => x.CategoryId == categoryId)
           .FirstOrDefaultAsync();

        public async Task<Product> GetProductByNameAsync(string name, decimal price, int categoryId) =>
            await FindByCondition(x => x.Name == name && x.Price == price && x.CategoryId == categoryId)
            .SingleOrDefaultAsync();

        public async Task<PaginationHelper<IEnumerable<Product>>> GetAllProductAsync(PagingParameter pagingParameter)
        {
            var allResult = FindAll();
            var filteredData = await allResult
                .OrderBy(x => x.Name)
                .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize)
                .Take(pagingParameter.PageSize)
                .ToListAsync();

            var result = new PaginationHelper<IEnumerable<Product>>
            {
                Data = filteredData,
                CurrentPage = pagingParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(allResult.Count() / (double)pagingParameter.PageSize),
                PageSize = pagingParameter.PageSize,
                TotalCount = allResult.Count()
            };

            return result;
        }

        public void UpdateProduct(Product product) => Update(product);
    }
}
