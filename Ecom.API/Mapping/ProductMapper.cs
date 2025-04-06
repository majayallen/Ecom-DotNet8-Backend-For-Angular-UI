using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapper : Profile
    {
        //Product
        public ProductMapper()
        {
            CreateMap<Product, ProductDTO>()
              .ForMember(x => x.CategoryName, op => op.MapFrom(src => src.Category.Name))
              .ReverseMap();
            CreateMap<Photo, PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO,Product>()
                .ForMember(x=>x.Photos, op => op.Ignore())
                .ReverseMap();
        }
    }
}
