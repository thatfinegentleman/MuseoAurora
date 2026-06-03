using System;
using System.ComponentModel.DataAnnotations;

namespace MuseoAurora.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Deve esserci almeno 1 partecipante.")]
        public int ParticipantsCount { get; set; } = 1;
        [Required]
        public DateTime? ReservationDate { get; set; } = DateTime.Now; 
        [Required]
        public string Status { get; set; } = "confermata";
        public Visitor Visitor { get; set; } = new Visitor();
        public GuidedTour GuidedTour { get; set; } = new GuidedTour();
    }
}