using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;

namespace Ecom.API.Mapping
{
    public class Mapper : Profile
    {
        public Mapper() { 
           
            // Category
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO,Category>().ReverseMap();

        }
    }
}
