using EcommProject.Data;
using System.Text.Json.Serialization;

namespace EcommProject.Model
{
    public class subCategoryModel
    {
        public int Id { get; set; }
        public string subCategory_Name { get; set; }
        public string subCategory_Description { get; set; }
        public int CategoryID { get; set; }

    }
}
