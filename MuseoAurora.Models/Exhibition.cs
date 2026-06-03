using System;
using System.ComponentModel.DataAnnotations;

namespace MuseoAurora.Models
{
    public class Exhibition
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo della mostra è obbligatorio.")]
        [StringLength(200, ErrorMessage = "Il titolo è troppo lungo.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descrizione è obbligatoria.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "La data di inizio è obbligatoria.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "La data di fine è obbligatoria.")]
        public DateTime? EndDate { get; set; }

        public string? ImageUrl { get; set; } 

        [Required]
        public string Status { get; set; } = "programmata"; 
    }
}