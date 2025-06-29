using TicketingSystem.Core.Entities;
using AutoMapper;
using TicketingSystem.Application.DTOs.TicketDtos;
using TicketingSystem.Application.DTOs.UserDtos;
using TicketingSystem.Application.DTOs.CustomerDtos;

namespace TicketingSystem.Application.Mapping
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : ""))
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.Tag != null ? src.Tag.Name : ""))
                .ForMember(dest => dest.AssignedToUserName, opt => opt.MapFrom(src => src.AssignedUser != null ? src.AssignedUser.Name : ""))
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Area.Name : ""))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Area.City.Name : ""))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Area.City.Country.Name : ""));
            CreateMap<TicketDto, Ticket>();

            CreateMap<CreateTicketDto, Ticket>().ReverseMap();
            CreateMap<UpdateTicketDto, Ticket>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();

            CreateMap<CustomerDto, Customer>().ReverseMap();

        }
    }
}
