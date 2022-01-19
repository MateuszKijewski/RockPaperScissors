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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
        }
    }
}