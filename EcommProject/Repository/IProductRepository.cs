using EcommProject.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace EcommProject.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllRecordAsync();

       // Task<List<ProductModel>> GetAllRecordAsync(FilterQuery  filterQuery);
        Task<ProductModel> GetProductByIdAsync(int productId);
        Task<int> AddProductAsync(ProductModel ecommModel);
        Task<String> UpdateProductAsync(ProductModel ecommModel);
        Task<String> UpdateBookPatchAsync(int id, JsonPatchDocument ecommModel);
        Task<String> DeleteBookByIdAsync(int ProductId);


    }
}
