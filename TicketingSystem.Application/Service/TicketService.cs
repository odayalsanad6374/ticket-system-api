using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.DTOs.TicketDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;

namespace TicketingSystem.Application.Service
{
    public class TicketService: ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepo;
        private readonly IMapper _mapper;

        public TicketService(IRepository<Ticket> ticketRepo, IMapper mapper)
        {
            _ticketRepo = ticketRepo;
            _mapper = mapper;
        }
        public async Task<TicketDto> GetByIdAsync(int id)
        {
            var ticket = await _ticketRepo.GetByIdAsync(id);
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task<IEnumerable<TicketDto>> GetAllAsync()
        {
            var tickets = await _ticketRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task AddAsync(CreateTicketDto dto)
        {
            var ticket = _mapper.Map<Ticket>(dto);
            await _ticketRepo.AddAsync(ticket);
        }

        public async Task UpdateAsync(UpdateTicketDto dto)
        {
            var existing = await _ticketRepo.GetByIdAsync(dto.Id);
            if (existing is null)
                throw new Exception("Ticket not found");

            _mapper.Map(dto, existing); // تحديث الكيان مباشرة
            await _ticketRepo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _ticketRepo.GetByIdAsync(id);
            if (ticket != null)
                await _ticketRepo.DeleteAsync(ticket);
        }
    }
}
