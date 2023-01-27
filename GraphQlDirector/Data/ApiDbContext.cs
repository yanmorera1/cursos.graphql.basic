using GraphQlDirector.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDirector.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Director>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Director)
                .HasForeignKey(m => m.DirectorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        public virtual DbSet<Video>? Videos { get; set; }
        public virtual DbSet<Streamer>? Streamers { get; set; }
        public virtual DbSet<Director>? Directores { get; set; }
    }
}
