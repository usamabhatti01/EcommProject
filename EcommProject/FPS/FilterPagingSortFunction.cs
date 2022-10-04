using EcommProject.Data;
using EcommProject.NewFolder;
using EcommProject.Query;

namespace EcommProject.FPS
{
    public class FilterPagingSortFunction
    {
        public IQueryable<Category> Filter(IQueryable <Category>  record, FPSQuery fPSQuery)
        {
            if (fPSQuery.Filter_Column != null)
            {
                switch (fPSQuery.Filter_Column)
                {
                    case "Category_Name":
                        record = record.Where(s => s.Category_Name == fPSQuery.Filter_Column_Name);
                        break;
                }
            }

            switch (fPSQuery.Sort_Type)
            {
                case "Id":
                    if (fPSQuery.Sort_By.ToLower() == "desc")
                    {
                        record = record.OrderByDescending(s => s.Id);
                    }
                    else
                    {
                        record = record.OrderBy(s => s.Id);
                    }
                    break;
                case "Category_Name":
                    if (fPSQuery.Sort_By.ToLower() == "desc")
                    {
                        record = record.OrderByDescending(s => s.Category_Name);
                    }
                    else
                    {
                        record = record.OrderBy(s => s.Id);
                    }
                    break;
                case "ProductId":
                    if (fPSQuery.Sort_By.ToLower() == "desc")
                    {
                        record = record.OrderByDescending(s => s.ProductId);
                    }
                    else
                    {
                        record = record.OrderBy(s => s.Id);
                    }
                    break;
                    record = record.Skip((int)((fPSQuery.PageNumber - 1) * fPSQuery.PageSize)).Take((int)fPSQuery.PageSize);

            }

            return record;
        }
    }
}
