namespace EcommProject.Data
{
    public class bridge
    {
        public int Category_Id { get; set; }
        public int subCategory_Id { get; set; }

        public Category category { get; set; }

        public subCategory SubCategory { get; set; }
    }
}
