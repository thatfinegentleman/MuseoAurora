using System.ComponentModel.DataAnnotations;

namespace MuseoAurora.Models
{
    public class Artwork
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo dell'opera è obbligatorio.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'autore dell'opera è obbligatorio.")]
        public string Author { get; set; } = string.Empty;

        public int? Year { get; set; }

        public string? Description { get; set; } 

        public string? Technique { get; set; }

        public string? ImageUrl { get; set; } 

        public Exhibition Exhibition { get; set; } = new Exhibition();
    }
}