using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommProject.Data
{
    public class subCategory
    {
        public int Id { get; set; }
        public string subCategory_Name { get; set; }
        public string subCategory_Description { get; set; }

  
        [JsonIgnore]
        public Category? Category { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }


    }
}
