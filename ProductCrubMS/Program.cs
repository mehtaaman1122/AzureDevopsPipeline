using Microsoft.EntityFrameworkCore;
using ProductCrubMS.Controllers;
using ProductCrubMS.Models;
using ProductCrubMS.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

if (app.Environment.IsDevelopment() || true) // 👈 force Swagger always
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCrubMS API v1");
        c.RoutePrefix = "swagger"; // 👈 required
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();