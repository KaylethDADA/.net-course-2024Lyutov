using BankSystem.Application.Interfaces;
using BankSystem.Application.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        b => b.MigrationsAssembly(typeof(BankSystemDbContext).Assembly.FullName)));

builder.Services.AddHttpClient();

builder.Services.AddScoped<CurrencyService>();
builder.Services.AddScoped<ICurrencyStorage, CurrencyStorage>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<IClientStorage, ClientStorage>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IStorage<Employee>, EmployeeStorage>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

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
