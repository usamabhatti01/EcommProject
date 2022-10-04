using EcommProject.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace EcommProject.Repository
{
    public interface ISubCategoryRepository
    {
        Task<List<subCategoryModel>> GetAllRecordAsync();
        Task<object> GetProductByIdAsync(int productId);
        Task<int> AddProductAsync(subCategoryModel subCategoryModel);
        Task<object> UpdateProductAsync(subCategoryModel subCategoryModel);
        Task<String> UpdateBookPatchAsync(int id, JsonPatchDocument subCategoryModel);
        Task<String> DeleteBookByIdAsync(int ProductId);
    }
}
