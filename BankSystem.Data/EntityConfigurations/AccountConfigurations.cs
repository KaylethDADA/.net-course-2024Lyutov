using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    public class AccountConfigurations : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.ClientId);

            builder.HasOne(x => x.Currency)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.CurrencyId);
        }
    }
}
