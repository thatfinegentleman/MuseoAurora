using System;
using System.ComponentModel.DataAnnotations;

namespace MuseoAurora.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "La quantità minima è 1.")]
        public int Quantity { get; set; } = 1;

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime? PurchaseDate { get; set; } = DateTime.Now;
        public Visitor Visitor { get; set; } = new Visitor();
        public TicketType TicketType { get; set; } = new TicketType();
        public Exhibition? Exhibition { get; set; } = new Exhibition();
        public GuidedTour? GuidedTour { get; set; } = new GuidedTour();
    }
}