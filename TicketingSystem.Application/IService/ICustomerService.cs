using TicketingSystem.Application.DTOs.CustomerDtos;

namespace TicketingSystem.Application.IService
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task AddAsync(CustomerDto customer);
        Task UpdateAsync(CustomerDto customer);
        Task DeleteAsync(int id);
    }
}
