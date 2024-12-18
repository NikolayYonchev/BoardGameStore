using Microsoft.EntityFrameworkCore;
using System;

namespace BoardGameStore.Models
{
    public class BoardGameStoreDbContext : DbContext
    {
        private readonly string connectionString;
        public BoardGameStoreDbContext(DbContextOptions options) : base(options)
        {
        }
        public BoardGameStoreDbContext(DbContextOptions<BoardGameStoreDbContext> options, IConfiguration configuration) : base(options)
        {
            
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
