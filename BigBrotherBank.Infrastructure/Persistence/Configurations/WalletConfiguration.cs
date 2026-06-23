using System;
using System.Collections.Generic;
using System.Text;
using BigBrotherBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BigBrotherBank.Infrastructure.Persistence.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets");
            builder.HasKey(wallet => wallet.Id);

            builder.Property(wallet => wallet.UserId)
                .IsRequired();

            builder.Property(wallet => wallet.Balance)
                .IsRequired()
                .HasColumnType("numeric(18,2)");

            builder.Property(wallet => wallet.Currency)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(16);

            builder.Property(wallet => wallet.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(32);

            builder.Property(wallet => wallet.CreatedAtUtc)
                .IsRequired();

            builder.HasIndex(wallet => wallet.UserId);
        }
    }
}
