
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RetailPricing.Data;
using RetailPricing.Repositories.Interfaces;
using RetailPricing.Repositories;
using RetailPricing.Services.Interfaces;
using RetailPricing.Services;

namespace RetailPricing.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
           
            builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IPricingService, PricingService>();
            builder.Services.AddScoped<IPricingRepository, PricingRepository>();
            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
