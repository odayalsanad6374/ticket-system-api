﻿using TicketingSystem.Core.Entities;

namespace TicketingSystem.Core.IRepository
{
    public interface ITokenRepository
    {
        string GenerateToken(User user);
    }
}
