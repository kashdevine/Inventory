using Inventory.Contracts;
using Inventory.Data;
using Inventory.Policies;
using Inventory.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Host.UseSerilog((ctx, config) =>
{
    config.MinimumLevel.Information();
    config.WriteTo.Console();
});

builder.Services.AddSingleton(new ServerPolicy().RetryDbForever);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Inventory API",
        Description = "An API for managing an inventory system."
    });
});

builder.Services.AddHealthChecks();

builder.Services.AddDbContext<InventoryContext>(opt =>
{
    var connString = builder.Configuration.GetConnectionString("InventoryDatabase");
    opt.UseSqlServer(connString);
});

builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IVendorRepository, VendorRepository>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
