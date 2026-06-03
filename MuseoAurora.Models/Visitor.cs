using System.ComponentModel.DataAnnotations;

namespace MuseoAurora.Models
{
    public class Visitor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'indirizzo email è obbligatorio.")]
        [EmailAddress(ErrorMessage = "Inserisci un indirizzo email valido.")]
        public string Email { get; set; } = string.Empty;
    }
}