using EcommProject.Data;
using EcommProject.Validation;
using System.ComponentModel.DataAnnotations;

namespace EcommProject.Model
{

    
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public List<Category>? Category { get; set; }

    }
}
