using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.DTOs.UserDtos;

namespace TicketingSystem.Application.IService
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task AddAsync(CreateUserDto user);
        Task UpdateAsync(UpdateUserDto user);
        Task DeleteAsync(int id);
        Task<UserDto?> GetByUsernameAsync(string username);
    }
}
