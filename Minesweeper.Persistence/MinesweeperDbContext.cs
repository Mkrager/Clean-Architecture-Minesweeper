using Microsoft.EntityFrameworkCore;
using Minesweeper.Domain.Common;
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<CreatableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
