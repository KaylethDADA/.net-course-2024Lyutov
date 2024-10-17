using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.FullName, fullName =>
            {
                fullName.Property(x => x.FirstName)
                .IsRequired();
                
                fullName.Property(x => x.LastName)
                .IsRequired();

                fullName.Property(x => x.MiddleName);
            });

            builder.Property(x => x.BirthDay)
                  .IsRequired()
                  .HasConversion(
                      v => v.ToUniversalTime(),
                      v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                  );

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.PassportNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x => x.PassportNumber)
                .IsUnique();

            builder.Property(x => x.Salary)
                .IsRequired();

            builder.Property(x => x.Contract)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
