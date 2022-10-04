using AutoMapper;
using EcommProject.Data;
using EcommProject.FPS;
using EcommProject.Model;
using EcommProject.NewFolder;
using EcommProject.Query;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;
using System.Data;
using System.Linq;

namespace EcommProject.Repository
{
    public class CategoryRepository:ICategoryRepository
    { 
        private readonly EcommStoreContext context;
        private readonly IMapper mapper;

        public CategoryRepository(EcommStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<CategoryModel>> GetAllRecordAsync()
        {
            var ecomm =  (from i in context.Categories
                               join it in context.subCategory on i.Id equals it.Id
                               into groupcollection
                               select new
                               {
                                   Category_Name=i.Category_Name,
                                   Category_des= i.Category_Description
                               }).ToListAsync();

            //var ecomm = await context.Categories.Include(i => i.SubCategories).ToListAsync();
            //var abc=ecomm.Select(i => i.Category_Description,);
            return mapper.Map<List<CategoryModel>>(ecomm);
        }

        public async Task<object> GetProductByIdAsync(String categoryId)
        {
            try
            {
                var record =  context.Categories.Where(i=>i.Category_Name==categoryId).ToList().ToLookup(s => s.Category_Name);
                /*var record = await (from i in context.Categories

                                   group i by i.Category_Name).ToListAsync();*/


                /* var record= await (from s in context.Categories
                                    where s.Id > 1 && s.Id < 5

                   select new
                              {
                                  s.Id,
                                  s.Category_Name,
                                  s.Category_Description,

                              }).ToListAsync();
   */
                //var record = await context.Categories.Include(i => i.SubCategories).ToListAsync();
                //var record = await context.Categories.Where(i=>i.Id==categoryId).SelectMany(x=>x.Id).ToListAsync();
                return record;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public async Task<object> CategoryFiltering(FilterQuery filter)

        {
            var record = await (from s in context.Categories
                                where s.Category_Name == filter.Category_Name
                                select new
                                {
                                    s.Id,
                                    s.Category_Name,
                                    s.Category_Description,
                                    s.ProductId

                                }).ToListAsync();


            return record;


        }
        public async Task<object> CategorySorting(SortingQuery sortingQuery)
        {

            var record = context.Categories.Where(s => true);

            switch (sortingQuery.sort_type)
            {
                case "Id":
                    if(sortingQuery.sort_by.ToLower()=="desc")
                    {
                         record =record.OrderByDescending(s => s.Id);
                    }
                    else
                    {
                        record = record.OrderBy(s => s.Id);
                    }
                    break;
                case "Category_Name":
                    if (sortingQuery.sort_by.ToLower() == "desc")
                    {
                        record = record.OrderByDescending(s => s.Category_Name);
                    }
                    else
                    {
                        record = record.OrderBy(s => s.Id);
                    }
                    break;
                case "ProductId":
                    if (sortingQuery.sort_by.ToLower() == "desc")
                    {
                        record = record.OrderByDescending(s => s.ProductId);
                    }
                    else
                    {
                        record = record.OrderBy(s => s.Id);
                    }
                    break;
               
            }


            return record.ToList();


        }

        public async Task<object> CategoryPagination(PaginationQuery pagination)

        { 
                var record = await (from s in context.Categories
                                 
                                    select new 
                                    {
                                        s.Id,
                                        s.Category_Name,
                                        s.Category_Description,
                                        s.ProductId

                                    }).OrderBy(s=>s.Id).Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
               



               
                return record;


        }
        public async Task<object> CategoryFPS(FPSQuery fPSQuery)
        {
           var record = context.Categories.AsQueryable().Where(s => true);

            FilterPagingSortFunction filterPagingSortFunction = new FilterPagingSortFunction();

            record = filterPagingSortFunction.Filter(record, fPSQuery);

            return record.ToList();


        }

        public async Task<int> AddProductAsync(CategoryModel categoryModel)
        {


            var record = mapper.Map<Category>(categoryModel);
            context.Categories.Add(record);
            await context.SaveChangesAsync();
            return record.Id;

        }

        public async Task<String> UpdateProductAsync(CategoryModel categoryModel)
        {
            var record = await context.Categories.FindAsync(categoryModel.Id);
            if (record != null)
            {

                record.Category_Name = categoryModel.Category_Name;
                record.Category_Description = categoryModel.Category_Description;
               
                await context.SaveChangesAsync();
                return "Data Update Successfully";
            }

            return "Not Data Not Present";

        }

        public async Task<String> UpdateBookPatchAsync(int id, JsonPatchDocument ecommModel)
        {
            var record = await context.Categories.FindAsync(id);
            if (record != null)
            {
                ecommModel.ApplyTo(record);

                await context.SaveChangesAsync();

                return "Partial data Update";
            }

            return "Not Data Not Present";

        }

        public async Task<String> DeleteBookByIdAsync(int categoryId)
        {
            var record = await context.Categories.FindAsync(categoryId);
            if (record != null)
            {
                context.Categories.Remove(record);
                await context.SaveChangesAsync();
                return "Data Deleted";
            }

            return "Not Data Not Present";

        }
    }
}
