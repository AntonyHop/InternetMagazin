using AutoMapper;
using InternetMagazine.Models;
using InternetMagazine.PL.DTO;

namespace InternetMagazine.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<UserDTO, RegistViewModel>().ReverseMap();
            CreateMap<UserDTO, AuthViewModel>().ReverseMap();
        }
    }
}
