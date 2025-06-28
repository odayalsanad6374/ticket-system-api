using AutoMapper;
using TicketingSystem.Application.DTOs.CustomerDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;

namespace TicketingSystem.Application.Service
{
    public class CustomerService: ICustomerService
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customer = await _customerRepo.GetAllAsync();
            return customer.Select(_mapper.Map<CustomerDto>);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task AddAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _customerRepo.AddAsync(customer);
        }

        public async Task UpdateAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _customerRepo.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _customerRepo.DeleteByIdAsync(id);
        }
    }
}
