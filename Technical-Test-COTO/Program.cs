using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Logger
builder.Logging
    .ClearProviders()
    .AddConsole()
    .AddDebug();

// Connection String
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"]
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Connection string no configurado");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
