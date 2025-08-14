using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using Domain.IRepository;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Technical_Test_COTO.Middlewares;

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

// Automapper
var mapperAssembly = typeof(MapperProfile).Assembly;
builder.Services.AddAutoMapper(cfg => { }, mapperAssembly);

// Inyecciones
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IAppDbInitializer), typeof(AppDbInitializer));
builder.Services.AddScoped(typeof(IReservaService), typeof(ReservaService));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Middlewares
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
