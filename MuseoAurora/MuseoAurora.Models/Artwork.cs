using System;
using System.Collections.Generic;
using System.Text;

namespace MuseoAurora.Models
{
    public class Artwork
    {
        public int Id { get; set; }
        public int ExhibitionId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Technique { get; set; }
        public string ImageUrl { get; set; }
    }
}
