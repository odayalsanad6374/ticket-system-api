using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Core.Enums;

namespace TicketingSystem.Application.DTOs.TicketDtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string? TicketNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
