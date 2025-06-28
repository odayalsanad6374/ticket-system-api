using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Core.IRepository
{
    public interface ITokenRepository
    {
        string GenerateToken(User user);
    }
}
