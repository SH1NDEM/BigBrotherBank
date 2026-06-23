using System;
using System.Collections.Generic;
using System.Text;
using BigBrotherBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BigBrotherBank.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(user => user.Email)
                .IsUnique();

            builder.Property(user => user.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(user => user.CreatedAtUtc)
                .IsRequired();
        }
    }
}
