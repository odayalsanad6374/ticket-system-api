using TicketingSystem.Application.DTOs.TicketDtos;
using TicketingSystem.Application.Models;

namespace TicketingSystem.Application.IService
{
    public interface ITicketService
    {
        Task<TicketDto> GetByIdAsync(int id);
        Task<PaginatedResult<TicketDto>> GetAllAsync(TicketFilterDto filter);
        Task AddAsync(CreateTicketDto dto);
        Task UpdateAsync(UpdateTicketDto dto);
        Task DeleteAsync(int id);
    }
}
