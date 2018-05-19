using AutoMapper;
using InternetMagazine.Models;
using InternetMagazine.PL.DTO;

namespace InternetMagazine.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomDTO, RoomViewModel>().ReverseMap();
            CreateMap<EventDTO, EventViewModel>().ReverseMap();
            CreateMap<UserDTO, RegistViewModel>().ReverseMap();
            CreateMap<UserDTO, AuthViewModel>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItemVIewModel>().ReverseMap();
        }
    }
}
