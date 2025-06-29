using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Core.Enums;

namespace TicketingSystem.Application.DTOs.TicketDtos
{
    public class TicketFilterDto
    {
        public string? TicketNumber { get; set; }
        public int? TechnicianId { get; set; }
        public int? CustomerId { get; set; }
        public TicketPriority? Priority { get; set; }
        public TicketStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}