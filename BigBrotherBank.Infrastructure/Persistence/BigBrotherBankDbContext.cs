using Microsoft.EntityFrameworkCore;
using BigBrotherBank.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BigBrotherBank.Infrastructure.Persistence
{
    public class BigBrotherBankDbContext : DbContext
    {
        public BigBrotherBankDbContext(DbContextOptions<BigBrotherBankDbContext> options) : base(options)
        {   
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BigBrotherBankDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
