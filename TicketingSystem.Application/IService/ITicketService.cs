using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.DTOs.TicketDtos;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Application.IService
{
    public interface ITicketService
    {
        Task<TicketDto> GetByIdAsync(int id);
        Task<IEnumerable<TicketDto>> GetAllAsync();
        Task AddAsync(CreateTicketDto dto);
        Task UpdateAsync(UpdateTicketDto dto);
        Task DeleteAsync(int id);
    }
}
