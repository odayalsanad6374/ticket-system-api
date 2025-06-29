using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore;
using TicketingSystem.Infrastructure.Data;
using TicketingSystem.Application.IService;
using TicketingSystem.Application.Service;
using TicketingSystem.Core.IRepository;
using TicketingSystem.Infrastructure.Repository;
using TicketingSystem.Application.Mapping;
using TicketingSystem.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TicketingSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //CorsPolicy
            var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            //Database context
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(TicketMappingProfile).Assembly);

            // Load JWT Auth Settings from appsettings.json
            var authSettings = builder.Configuration.GetSection("JwtSettings").Get<AuthSettings>();

            builder.Services.Configure<AuthSettings>(
                builder.Configuration.GetSection("JwtSettings"));

            //Configure JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authSettings.Issuer,
                        ValidAudience = authSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret))
                    };
                });

            // Add Repository Injection
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();

            //Dependency Injection
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();

            //Controllers & Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer(); // ??? ????
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TicketingSystem API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            // ? Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketingSystem API v1");
                    c.RoutePrefix = "swagger"; // ???? ??? /swagger
                });
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
