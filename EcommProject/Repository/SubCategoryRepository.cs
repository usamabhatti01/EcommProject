using AutoMapper;
using EcommProject.Data;
using EcommProject.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace EcommProject.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly EcommStoreContext context;
        private readonly IMapper mapper;

        public SubCategoryRepository(EcommStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<subCategoryModel>> GetAllRecordAsync()
        {
            var ecomm = await context.subCategory.

                Where(s => s.Id > 9).ToListAsync();

          

            return mapper.Map<List<subCategoryModel>>(ecomm);
        }

        public async Task<object> GetProductByIdAsync(int subcatId)
        {

            try
            {
                    var record = await context.subCategory.Where(x => x.Id == subcatId).Select(x => x.subCategory_Name).FirstOrDefaultAsync();
                //var record = await context.subCategories.Where(x => x.Id == subcatId).Select(x => x.subCategory_Name).FirstOrDefaultAsync();
                //var record = await context.subCategories.Where(x => x.Id == subcatId).AsNoTracking().FirstOrDefaultAsync();
                //var record = await context.subCategories.FindAsync(subcatId);
                return (record);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<int> AddProductAsync(subCategoryModel subCategoryModel)
        {


            var record = mapper.Map<subCategory>(subCategoryModel);
            context.subCategory.Add(record);
            await context.SaveChangesAsync();
            return record.Id;

        }

        public async Task<object> UpdateProductAsync(subCategoryModel subCategoryModel)
        {
            /* var record = await context.subCategories.FindAsync(subCategoryModel.Id);
             if (record != null)
             {

                 record.subCategory_Name = subCategoryModel.subCategory_Name;
                 record.subCategory_Description = subCategoryModel.subCategory_Description;

                 await context.SaveChangesAsync();
                 return "Data Update Successfully";
             }

 */
           

            var books = await context.subCategory.FromSqlRaw(
                              $"UPDATE subCategories" +
                              "SET subCategory_Name ={subCategoryModel.subCategory_Name} ," +
                              "subCategory_Description={subCategoryModel.subCategory_Description}," +
                              " CategoryId={subCategoryModel.CategoryId}," +
                              "WHERE Id =={subCategoryModel.Id};")
                               .ToListAsync();

          
              return books;

        }

        public async Task<String> UpdateBookPatchAsync(int id, JsonPatchDocument subCategoryModel)
        {
            var record = await context.subCategory.FindAsync(id);
            if (record != null)
            {
                subCategoryModel.ApplyTo(record);

                await context.SaveChangesAsync();

                return "Partial data Update";
            }

            return "Not Data Not Present";

        }

        public async Task<String> DeleteBookByIdAsync(int subcatId)
        {
            var record = await context.subCategory.FindAsync(subcatId);
            if (record != null)
            {
                context.subCategory.Remove(record);
                await context.SaveChangesAsync();
                return "Data Deleted";
            }

            return "Not Data Not Present";

        }
    }
}
