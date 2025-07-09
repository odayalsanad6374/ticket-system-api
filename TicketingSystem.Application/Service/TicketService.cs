using AutoMapper;
using TicketingSystem.Application.DTOs.TicketDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Application.Models;
using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;
using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Application.Service
{
    public class TicketService: ITicketService
    {
        private readonly IRepository<Ticket> _genericRepo;
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepo;

        public TicketService(IRepository<Ticket> genericRepo, IMapper mapper, ITicketRepository ticketRepo)
        {
            _genericRepo = genericRepo;
            _mapper = mapper;
            _ticketRepo = ticketRepo;
        }
        public async Task<TicketDto> GetByIdAsync(int id)
        {
            //var ticket = await _genericRepo.GetByIdAsync(id);
            //return _mapper.Map<TicketDto>(ticket);
            var ticket = await _ticketRepo.Query()
            .Include(t => t.Customer)
                .ThenInclude(c => c.Area)
                    .ThenInclude(a => a.City)
                        .ThenInclude(ci => ci.Country)
            .Include(t => t.AssignedUser)
            .Include(t => t.Tag)
            .FirstOrDefaultAsync(t => t.Id == id);

            return ticket == null ? null : _mapper.Map<TicketDto>(ticket);
        }

        public async Task<PaginatedResult<TicketDto>> GetAllAsync(TicketFilterDto filter)
        {
            var query = _ticketRepo.Query();

            if (!string.IsNullOrWhiteSpace(filter.TicketNumber))
                query = query.Where(t => t.TicketNumber!.Contains(filter.TicketNumber));

            if (filter.TechnicianId.HasValue)
                query = query.Where(t => t.AssignedToUserId == filter.TechnicianId);

            if (filter.CustomerId.HasValue)
                query = query.Where(t => t.CustomerId == filter.CustomerId);

            if (filter.Priority.HasValue)
                query = query.Where(t => t.Priority == filter.Priority);

            if (filter.Status.HasValue)
                query = query.Where(t => t.Status == filter.Status);

            if (filter.StartDate.HasValue)
                query = query.Where(t => t.CreateDate >= filter.StartDate);

            if (filter.EndDate.HasValue)
                query = query.Where(t => t.CreateDate <= filter.EndDate);

            var totalCount = await query.CountAsync();

            var tickets = await query
                .Include(t => t.Customer)
                    .ThenInclude(c => c.Area)
                        .ThenInclude(a => a.City)
                            .ThenInclude(c => c.Country)
                .Include(t => t.AssignedUser)
                .Include(t => t.Tag)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var ticketDtos = _mapper.Map<IEnumerable<TicketDto>>(tickets);

            return new PaginatedResult<TicketDto>
            {
                Items = ticketDtos,
                TotalCount = totalCount
            };
        }


        public async Task AddAsync(CreateTicketDto dto)
        {
            var ticket = _mapper.Map<Ticket>(dto);
            await _genericRepo.AddAsync(ticket);
        }

        public async Task UpdateAsync(UpdateTicketDto dto)
        {
            var existing = await _genericRepo.GetByIdAsync(dto.Id);
            if (existing is null)
                throw new Exception("Ticket not found");

            _mapper.Map(dto, existing); // تحديث الكيان مباشرة
            await _genericRepo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _genericRepo.GetByIdAsync(id);
            if (ticket != null)
                await _genericRepo.DeleteAsync(ticket);
        }
    }
}
