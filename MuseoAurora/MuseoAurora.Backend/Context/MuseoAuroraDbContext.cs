using Microsoft.EntityFrameworkCore;
using MuseoAurora.Models;

namespace MuseoAurora.Backend.Context
{
    public class MuseoAuroraDbContext : DbContext
    {
        public MuseoAuroraDbContext(DbContextOptions<MuseoAuroraDbContext> options)
            : base(options)
        {
        }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<GuidedTour> GuidedTours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
