using Microsoft.EntityFrameworkCore;

namespace TimeManagement.V2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<StudySessions> StudySessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudySessions>()
                .HasOne(s => s.Module)
                .WithMany(m => m.StudySessions)
                .HasForeignKey(s => s.ModuleID);
        }
    }
}
