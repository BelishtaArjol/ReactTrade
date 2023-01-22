using Entities.Helpers;
using Entities.Models;
using Entities.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository
    {
        Task<PaginationHelper<IEnumerable<Category>>> GetAllCategoryAsync(PagingParameter pagingParameter);
        Task<Category> GetCategoryByCodeAsync(string code);
        Task<Category> GetCategoryByIdAsync(int id);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
