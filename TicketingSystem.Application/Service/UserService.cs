using AutoMapper;
using TicketingSystem.Application.DTOs.UserDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;

namespace TicketingSystem.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Repository<User>().GetAllAsync();
            return users.Select(_mapper.Map<UserDto>);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task AddAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<User>().DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            var users = await _unitOfWork.Repository<User>().FindAsync(u => u.Name == username);
            var user = users.FirstOrDefault();
            return user == null ? null : _mapper.Map<UserDto>(user);
        }
    }
}