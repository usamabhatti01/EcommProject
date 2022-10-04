using EcommProject.Model;
using EcommProject.NewFolder;
using EcommProject.Query;
using Microsoft.AspNetCore.JsonPatch;

namespace EcommProject.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAllRecordAsync();
        //Task<List<CategoryModel>> GetAllRecordAsync(FilterQuery filter);

        Task<object> GetProductByIdAsync(String productId);
        Task<object> CategorySorting(SortingQuery sorting);
        Task<object> CategoryFiltering(FilterQuery filter);
        Task<object> CategoryPagination(PaginationQuery pagination);

        Task<object> CategoryFPS(FPSQuery fPSQuery);
        Task<int> AddProductAsync(CategoryModel categoryModel);
        Task<String> UpdateProductAsync(CategoryModel categoryModel);
        Task<String> UpdateBookPatchAsync(int id, JsonPatchDocument categoryModel);
        Task<String> DeleteBookByIdAsync(int ProductId);
    }
}
