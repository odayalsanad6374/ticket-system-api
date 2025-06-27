using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.DTOs.UserDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;

namespace TicketingSystem.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(_mapper.Map<UserDto>);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task AddAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _userRepo.AddAsync(user);
        }

        public async Task UpdateAsync(UpdateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _userRepo.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepo.DeleteByIdAsync(id);
        }
        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            var users = await _userRepo.FindAsync(u => u.Name == username);
            var user = users.FirstOrDefault();
            return user == null ? null : _mapper.Map<UserDto>(user);
        }
    }
}