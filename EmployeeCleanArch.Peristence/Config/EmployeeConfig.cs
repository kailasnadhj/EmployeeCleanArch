using EmployeeCleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeCleanArch.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Designation)
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.PhoneNumber)
                .IsRequired();

            builder.Property(e => e.Gender)
                .HasMaxLength(50);

            builder.Property(e => e.Nationality)
                .HasMaxLength(50);

            builder.Property(e => e.Salary)
                .IsRequired();

            builder.Property(e => e.DateOfBirth)
                .IsRequired();

            builder.Property(e => e.DateOfJoining)
                .IsRequired();

            builder.Property(e => e.DateOfDeparture);

            builder.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.Property(e => e.IsDeleted)
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .HasMaxLength(255);

            builder.Property(e => e.CreatedDate)
                .IsRequired();

            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(255);

            builder.Property(e => e.UpdatedDate);
        }
    }
}
