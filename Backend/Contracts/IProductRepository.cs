using Entities.Helpers;
using Entities.Models;
using Entities.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<PaginationHelper<IEnumerable<Product>>> GetAllProductAsync(PagingParameter pagingParameter);
        Task<Product> GetProductByNameAsync(string name, decimal price, int categoryId);
        Task<Product> FindCategoryforProductAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
