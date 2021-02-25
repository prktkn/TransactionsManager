using Microsoft.EntityFrameworkCore;
using TransactionsManager.DAL.Models;

namespace TransactionsManager
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionTemp> TransactionsTemp { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Transaction>().Property(s => s.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasKey(u => u.Login);

            modelBuilder.Entity<TransactionTemp>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<TransactionTemp>().Property(s => s.Id).ValueGeneratedNever();
        }
    }
}
