﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.API.ActionFilters;
using TicketingSystem.Application.DTOs.TicketDtos;
using TicketingSystem.Application.IService;
using TicketingSystem.Application.Models;

namespace TicketingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet]
        [RoleIdAuthorize(1)]
        public async Task<ActionResult<PaginatedResult<TicketDto>>> GetAll([FromQuery] TicketFilterDto filter)
        {
            var tickets = await _ticketService.GetAllAsync(filter);
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetById(int id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto dto)
        {
            await _ticketService.AddAsync(dto);
            return Ok(new { message = "Created successfully" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTicketDto dto)
        {
            await _ticketService.UpdateAsync(dto);
            return Ok(new { message = "Updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ticketService.DeleteAsync(id);
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
