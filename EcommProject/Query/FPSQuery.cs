namespace EcommProject.Query
{
    public class FPSQuery
    {
        public string? Sort_Type { get; set; }
        public string? Sort_By { get; set; }

        //pagination
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        //filter
        public string? Filter_Column { get; set; }
        public string? Filter_Column_Name { get; set; }
        // public int? Product_Id { get; set; }

        public bool HaveFilter => !string.IsNullOrEmpty(Filter_Column)

            || !string.IsNullOrEmpty(Filter_Column_Name)

            || !string.IsNullOrEmpty(Sort_Type)
            
            || !string.IsNullOrEmpty(Sort_By);


        public FPSQuery()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
    }
}
