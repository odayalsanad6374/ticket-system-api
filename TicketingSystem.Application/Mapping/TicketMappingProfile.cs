using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TicketingSystem.Core.Entities;
using AutoMapper;
using TicketingSystem.Application.DTOs.TicketDtos;
using TicketingSystem.Application.DTOs.UserDtos;

namespace TicketingSystem.Application.Mapping
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<CreateTicketDto, Ticket>().ReverseMap();
            CreateMap<UpdateTicketDto, Ticket>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
