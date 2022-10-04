using AutoMapper;
using EcommProject.Data;
using EcommProject.Model;

namespace ecommer.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {

        CreateMap<Product, ProductModel>().ReverseMap();
        CreateMap<Category,CategoryModel>().ReverseMap();
        CreateMap<subCategory, subCategoryModel>().ReverseMap();


            //CreateMap<Ecomm, EcommModel>()
            //        .ForMember(dest => dest.ProductName,
            //                   opt => opt.MapFrom(src => src.ProductDescription));
        }
    }
}
