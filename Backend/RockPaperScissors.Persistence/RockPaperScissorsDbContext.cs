using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Persistence
{
    public class RockPaperScissorsDbContext : DbContext
    {
        public RockPaperScissorsDbContext(DbContextOptions<RockPaperScissorsDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Game> Game { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();

            modelBuilder
                .Entity<Game>()
                .Property(x => x.HostId)
                .IsRequired();
            modelBuilder
                .Entity<Game>()
                .HasOne(x => x.Host)
                .WithMany()
                .HasForeignKey(x => x.HostId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .Entity<Game>()
                .HasOne(x => x.Guest)
                .WithMany()
                .HasForeignKey(x => x.GuestId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}