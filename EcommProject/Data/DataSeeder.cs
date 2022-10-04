using Microsoft.EntityFrameworkCore;

namespace EcommProject.Data
{
    public class DataSeeder
    {
        private readonly EcommStoreContext ecommStoreContext;

        public DataSeeder(EcommStoreContext ecommStoreContext )
        {
            this.ecommStoreContext = ecommStoreContext;
        }

        /*public void seed()
        {
            if (!ecommStoreContext.subCategory.Any())
            {
                var subCategories = new List<subCategory>()
                { new subCategory
                    {
                        subCategory_Name = "new-1",
                        subCategory_Description = "abc",
                        CategoryId=2
                    },
                    new subCategory()
                    {
                        subCategory_Name = "new-1",
                        subCategory_Description = "abc",
                        CategoryId=2
                    }
                };
                ecommStoreContext.subCategory.AddRange(subCategories);
                ecommStoreContext.SaveChanges();
            }
        }*/
    }

}
