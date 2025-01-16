using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCleanArch.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new DepartmentConfig());
            builder.ApplyConfiguration(new EmployeeConfig());
        }
    }
}
