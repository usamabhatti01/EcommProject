using AutoMapper;
using EcommProject.Data;
using EcommProject.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using static System.Reflection.Metadata.BlobBuilder;

namespace EcommProject.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommStoreContext context;
        private readonly IMapper mapper;

        public ProductRepository(EcommStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<ProductModel>> GetAllRecordAsync()
        {
            var ecomm = await context.Products.Include(i => i.Category).ThenInclude(ti => ti.SubCategories).ToListAsync();
            //mapper.Map<List<Category>>(ecomm);
            //mapper.Map<List<subCategory>>(ecomm);
            return mapper.Map<List<ProductModel>>(ecomm);
        }
        public async Task<ProductModel> GetProductByIdAsync(int productId)
        {

            try
            {

                //var record = await context.Products.FromSqlRaw("select * from Products where Id={productId}").FirstOrDefault();
                var record = await context.Products.Where(t=>t.Id== productId).Include(i => i.Category).ThenInclude(ti => ti.SubCategories).FirstOrDefaultAsync();
                //var a =mapper.Map<List<CategoryModel>>(record);
                //var b =mapper.Map<List<subCategory>>(record);
                return mapper.Map<ProductModel>(record);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<int> AddProductAsync(ProductModel productModel)
        {
           
            var record = mapper.Map<Product>(productModel);

            context.Products.Add(record);
            await context.SaveChangesAsync();
            return record.Id;

        }

        public async Task<String> UpdateProductAsync(ProductModel productModel)
        {
            var record = await context.Products.FindAsync(productModel.Id);
            if (record != null)
            {

                record.ProductName = productModel.ProductName;
                record.ProductDescription = productModel.ProductDescription;
                record.ProductPrice = productModel.ProductPrice;
               

                await context.SaveChangesAsync();
                return "Data Update Successfully";
            }

            return "Not Data Not Present";

        }

        public async Task<String> UpdateBookPatchAsync(int id, JsonPatchDocument ecommModel)
        {
            var record = await context.Products.FindAsync(id);
            if (record != null)
            {
                ecommModel.ApplyTo(record);

                await context.SaveChangesAsync();

                return "Partial data Update";
            }

            return "Not Data Not Present";

        }

        public async Task<String> DeleteBookByIdAsync(int ProductId)
        {
            var record = await context.Products.FindAsync(ProductId);
            if (record != null)
            {
                context.Products.Remove(record);
                await context.SaveChangesAsync();
                return "Data Deleted";
            }

            return "Not Data Not Present";

        }



    }
}
