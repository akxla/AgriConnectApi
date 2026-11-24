using Microsoft.EntityFrameworkCore;
using AgriConnectApi.Data;
using AgriConnectApi.IServices;
using AgriConnectApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "Database connection string not found.")));

builder.Services.AddScoped<
    IOfficerDashboardService,
    OfficerDashboardService>();

builder.Services.AddScoped<
    IQuotationVerificationService,
    QuotationVerificationService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();