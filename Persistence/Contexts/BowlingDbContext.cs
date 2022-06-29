using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class BowlingDbContext : DbContext
    {
        public DbSet<Games>? Games { get; set; }
        public DbSet<Scores>? Scores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory;
            optionsBuilder.UseSqlite($"Data Source={dbPath}\\Bowling.db");
        }
    }
}
