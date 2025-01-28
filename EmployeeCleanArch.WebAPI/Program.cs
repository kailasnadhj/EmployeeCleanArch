using EmployeeCleanArch.Application.Features.Departments.Commands.AddNewDepartment;
using EmployeeCleanArch.Application.Features.Departments.Queries.GetAllDepartments;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Infrastructure.Data;
using EmployeeCleanArch.Peristence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "CleanArch.Infrastructure", "logs");

var solutionDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..")); // 4 levels up from the bin directory
var logDirectory = Path.Combine(solutionDirectory, "CleanArch.Infrastructure", "logs");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Logs to the console
    .WriteTo.File(Path.Combine(logDirectory, "app-log.txt"), rollingInterval: RollingInterval.Day) // Logs to file
    .CreateLogger();

builder.Logging.AddSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddValidatorsFromAssemblyContaining<AddNewDepartmentValidator>();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetAllDepartmentsQueryHandler).Assembly));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
