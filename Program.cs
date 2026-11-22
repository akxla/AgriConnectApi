using Microsoft.EntityFrameworkCore;
using AgriConnectApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "Database connection string not found.")));

var app = builder.Build();

app.MapControllers();

app.Run();