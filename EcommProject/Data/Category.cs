using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommProject.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Category_Name { get; set; }

        public string Category_Description { get; set; }
        public List<subCategory>? SubCategories { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
    

    }
}
