using Microsoft.EntityFrameworkCore;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Persistence
{
    public class MinesweeperDbContext : DbContext
    {
        public MinesweeperDbContext(DbContextOptions<MinesweeperDbContext> options)
            : base(options)
        {
        }

        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MinesweeperDbContext).Assembly);

            modelBuilder.Entity<LeaderboardEntry>()
                .Property(e => e.CreatedAt)
                .HasColumnName("AchievedAt");
        }
    }
}
