namespace EcommProject.NewFolder
{
    public class FilterQuery
    {
        public string? Category_Name { get; set; }
        public int? Product_Id { get; set; }

        public bool HaveFilter => !string.IsNullOrEmpty(Category_Name);
    }
}
