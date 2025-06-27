using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore;
using TicketingSystem.Infrastructure.Data;
using TicketingSystem.Application.IService;
using TicketingSystem.Application.Service;
using TicketingSystem.Core.IRepository;
using TicketingSystem.Infrastructure.Repository;
using TicketingSystem.Application.Mapping;

namespace TicketingSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Database context
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //2. AutoMapper
            builder.Services.AddAutoMapper(typeof(TicketMappingProfile).Assembly);

            // Add Repository Injection
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Dependency Injection
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IUserService, UserService>();

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

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
