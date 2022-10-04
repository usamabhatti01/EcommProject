namespace EcommProject.Data
{
    public class Product
    {
        
        public int Id { get; set; }
        public string ProductName { get; set; }      
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }       
        public List<Category>? Category { get; set; }
        
    }
}
