using Microsoft.EntityFrameworkCore;
using MuseoAurora.Models;

namespace MuseoAurora.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<GuidedTour> GuidedTours { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Visitor>()
                .HasIndex(v => v.Email)
                .IsUnique();
        }
    }
}