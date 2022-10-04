using EcommProject.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace EcommProject.Model
{
    public class CategoryModel
    {
       
        public int Id { get; set; }
        public string Category_Name { get; set; }

        public string Category_Description { get; set; }
        public List<subCategoryModel>? SubCategories { get; set; }
        public int ProductId { get; set; }

    }
}
