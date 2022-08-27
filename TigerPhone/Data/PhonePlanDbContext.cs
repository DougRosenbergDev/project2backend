using Microsoft.EntityFrameworkCore;
using TigerPhone.Models;

namespace TigerPhone.Data
{
    public class PhonePlanDbContext : DbContext
    {
        public PhonePlanDbContext(DbContextOptions<PhonePlanDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Device> Devices { get; set; }
        //bill (b) could be the join entity...
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Plan>()
                .HasMany(ub => ub.Devices)
                .WithMany(u => u.Plans)
                .HasForeignKey(u => u.Id);

            modelBuilder.Entity<Plan>()
                .HasMany(ub => ub.Users)
                .WithMany(b => b.Devices)
                .HasForeignKey(UriBuilder => UriBuilder.UserId);
        }
    }
}
