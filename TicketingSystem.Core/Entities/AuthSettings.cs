﻿
namespace TicketingSystem.Core.Entities
{
    public class AuthSettings
    {
        public string Secret { get; set; } = string.Empty;
        public int TokenExpirationMinutes { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int RefreshTokenExpirationDays { get; set; }
    }
}
