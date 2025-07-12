using AutoMapper;
using TicketingSystem.Application.DTOs.CustomerDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;

namespace TicketingSystem.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
            return customers.Select(_mapper.Map<CustomerDto>);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task AddAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _unitOfWork.Repository<Customer>().UpdateAsync(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<Customer>().DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
