using BankSystem.Application.Dto.ClientDto;
using BankSystem.Application.Dto.EmployeeDto;
using BankSystem.Application.FluentValidations.ClientValidations;
using BankSystem.Application.FluentValidations.EmployeeValidations;
using BankSystem.Application.Interfaces;
using BankSystem.Application.Mapping;
using BankSystem.Application.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BankSystemDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(BankSystemDbContext).Assembly.FullName)));

builder.Services.AddScoped<ICurrencyStorage, CurrencyStorage>();

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<IClientStorage, ClientStorage>();
builder.Services.AddAutoMapper(typeof(ClientMappingProfile));

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IStorage<Employee>, EmployeeStorage>();
builder.Services.AddAutoMapper(typeof(EmployeeMappingProfile));


builder.Services.AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<CreateClientRequestValidator>();
    config.RegisterValidatorsFromAssemblyContaining<UpdateClientRequestValidator>();
    config.RegisterValidatorsFromAssemblyContaining<CreateEmployeeRequestValidator>();
    config.RegisterValidatorsFromAssemblyContaining<UpdateEmployeeRequestValidator>();
});

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
