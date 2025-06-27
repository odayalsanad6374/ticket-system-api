using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Core.Enums;

namespace TicketingSystem.Application.DTOs.TicketDtos
{
    public class UpdateTicketDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public int CustomerId { get; set; }
        public int? AssignedToUserId { get; set; }
        public int TagId { get; set; }
    }
}
