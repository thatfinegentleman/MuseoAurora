using System;
using System.ComponentModel.DataAnnotations;

namespace MuseoAurora.Models
{
    public class GuidedTour
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo della visita guidata è obbligatorio.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "L'orario di inizio è obbligatorio.")]
        public DateTime? StartTime { get; set; }

        [Required]
        [Range(1, 1440, ErrorMessage = "La durata deve essere di almeno 1 minuto.")]
        public int DurationMinutes { get; set; } = 60; 

        [Required(ErrorMessage = "Il nome della guida è obbligatorio.")]
        public string GuideName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Il numero massimo di partecipanti è obbligatorio.")]
        [Range(1, 200, ErrorMessage = "Inserisci un numero di partecipanti valido.")]
        public int MaxParticipants { get; set; }
        public Exhibition Exhibition { get; set; } = new Exhibition();
    }
}