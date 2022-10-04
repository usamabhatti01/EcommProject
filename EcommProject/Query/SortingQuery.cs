namespace EcommProject.NewFolder
{
    public class SortingQuery
    {
        public string? sort_type { get; set; }
        public string? sort_by { get; set; }
        public bool HaveFilter => !string.IsNullOrEmpty(sort_by) || !string.IsNullOrEmpty(sort_type);
    }
}
