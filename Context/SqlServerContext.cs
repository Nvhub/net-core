using Microsoft.EntityFrameworkCore;
using webApi.Models;
using webApi.Models.Wallet;

namespace webApi.Context
{
    public class SqlServerContext : DbContext
    {

        public SqlServerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PhoneBook> PhoneBooks { get; set; }

        public DbSet<WalletDoller> WalletDollers { get; set; }

        public DbSet<WalletRial> WalletRials { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WalletRial>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }

    }
}
