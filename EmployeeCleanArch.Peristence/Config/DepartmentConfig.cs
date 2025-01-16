using EmployeeCleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeCleanArch.Infrastructure.Persistence.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Location)
                .HasMaxLength(100);

            builder.Property(d => d.Status)
                .HasMaxLength(50);

            builder.Property(d => d.MaxCapacity)
                .IsRequired();

            builder.Property(d => d.IsDeleted)
                .IsRequired();

            builder.HasMany(d => d.Employees)
            .WithOne(e => e.Department);

            builder.Property(d => d.CreatedBy)
                .HasMaxLength(255);

            builder.Property(d => d.CreatedDate)
                .IsRequired();

            builder.Property(d => d.UpdatedBy)
                .HasMaxLength(255);

            builder.Property(d => d.UpdatedDate);
        }
    }
}
