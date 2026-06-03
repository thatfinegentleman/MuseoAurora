using System.ComponentModel.DataAnnotations;

namespace MuseoAurora.Models
{
    public class TicketType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della tipologia di biglietto è obbligatorio.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Il prezzo è obbligatorio.")]
        [Range(0.00, 1000.00, ErrorMessage = "Il prezzo deve essere maggiore o uguale a 0.")]
        public decimal Price { get; set; }
    }
}