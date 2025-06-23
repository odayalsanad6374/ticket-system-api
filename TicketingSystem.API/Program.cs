using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore;
using TicketingSystem.Infrastructure.Data;

namespace TicketingSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // DbContext
            builder.Services.AddDbContext<AppDbContect>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.

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
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
