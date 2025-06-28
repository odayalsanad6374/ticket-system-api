using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.DTOs.CustomerDtos;
using TicketingSystem.Application.DTOs.UserDtos;
using TicketingSystem.Application.IService;

namespace TicketingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customer = await _customerService.GetAllAsync();
            return Ok(customer);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto dto)
        {
            await _customerService.AddAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerDto dto)
        {
            await _customerService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteAsync(id);
            return Ok();
        }
    }
}
