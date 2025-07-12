using AutoMapper;
using TicketingSystem.Application.DTOs.TicketDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Application.Models;
using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;
using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Application.Service
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TicketDto> GetByIdAsync(int id)
        {
            var ticket = await _unitOfWork.TicketRepository.Query()
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
            var query = _unitOfWork.TicketRepository.Query();

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
            await _unitOfWork.Repository<Ticket>().AddAsync(ticket);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTicketDto dto)
        {
            var existing = await _unitOfWork.Repository<Ticket>().GetByIdAsync(dto.Id);
            if (existing is null)
                throw new Exception("Ticket not found");

            _mapper.Map(dto, existing);
            await _unitOfWork.Repository<Ticket>().UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(id);
            if (ticket != null)
            {
                await _unitOfWork.Repository<Ticket>().DeleteAsync(ticket);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
