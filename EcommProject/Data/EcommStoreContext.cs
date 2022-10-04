using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Reflection.Metadata.BlobBuilder;

namespace EcommProject.Data
{
    public class EcommStoreContext: DbContext
    {
        public EcommStoreContext(DbContextOptions<EcommStoreContext> option): base(option) { }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<subCategory> subCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<subCategory>().HasData(
               new subCategory
               {
                   Id = 15,
                   subCategory_Name = "subCategory_Name-1",
                   subCategory_Description = "subCategory_Name",
                   CategoryId = 10
               },
                   new subCategory()
                   {
                       Id = 16,
                       subCategory_Name = "subCategory_Name-1",
                       subCategory_Description = "abc",
                       CategoryId = 11
                   }
               );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Category_Name-1",
                    ProductDescription = "Category_Name",
                    ProductPrice = 10
                },
                    new Product()
                    {
                        Id = 2,
                        ProductName = "ProductName-1",
                        ProductDescription = "ProductName",
                        ProductPrice = 20
                    }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 10,
                    Category_Name = "Category_Name-1",
                    Category_Description = "Category_Name",
                    ProductId = 1
                },
                    new Category()
                    {
                        Id = 11,
                        Category_Name = "Category_Name",
                        Category_Description = "abc",
                        ProductId = 2
                    }
                );
        }
    }

    
   
}


