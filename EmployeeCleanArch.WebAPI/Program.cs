using EmployeeCleanArch.Application.Features.Departments.Commands.AddNewDepartment;
using EmployeeCleanArch.Application.Features.Departments.Queries.GetAllDepartments;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Infrastructure.Data;
using EmployeeCleanArch.Peristence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
//builder.Services.AddTransient<IValidator<CreateDepartmentDTO>, AddNewDepartmentValidator>();



//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetAllDepartmentsQueryHandler).Assembly));
/*builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(EmployeeCleanArch.Application.Features.Departments.Commands.AddNewDepartmentCommandHandler).Assembly));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(EmployeeCleanArch.Application.Features.Departments.Queries.GetDepartmentByIdQueryHandler).Assembly));*/
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
