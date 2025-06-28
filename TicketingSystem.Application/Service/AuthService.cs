using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using TicketingSystem.Application.DTOs.AuthDto;
using TicketingSystem.Application.DTOs.LoginDto;
using TicketingSystem.Application.DTOs.UserDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;

namespace TicketingSystem.Application.Service
{
    public class AuthService: IAuthService
    {
        private readonly IRepository<User> _userRepo;
        private readonly ITokenRepository _tokenService;
        private readonly IMapper _mapper;

        public AuthService(IRepository<User> userRepo, ITokenRepository tokenService, IMapper mapper)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = (await _userRepo.FindAsync(u => u.Email == loginDto.Email)).FirstOrDefault();
            var pass = HashPassword(loginDto.Password);

            if (user == null || user.PasswordHash != pass)
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                User = _mapper.Map<UserDto>(user)
            };
        }

        private string HashPassword(string pass)
        {
            if (pass == null)
            {
                return "";
            }
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
