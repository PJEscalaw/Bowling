using Business;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Contexts;
using PriceData.WebApi.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

static string GetSerilogFileConfig()
{
    string logPath = AppDomain.CurrentDomain.BaseDirectory;
    return @$"{logPath}\logs\log-.txt";
}

builder.Host.UseSerilog((_, lc) => lc
    .Enrich.FromLogContext()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.File(GetSerilogFileConfig(), rollingInterval: RollingInterval.Day));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence();
builder.Services.AddBusiness();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<BowlingDbContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
