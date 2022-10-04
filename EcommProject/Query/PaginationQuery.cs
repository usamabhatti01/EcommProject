namespace EcommProject.Query
{
    public class PaginationQuery
    {       
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public PaginationQuery()
            {
                this.PageNumber = 1;
                this.PageSize = 10;
            }
    }
}
