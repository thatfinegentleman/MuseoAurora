using System;
using System.Collections.Generic;
using System.Text;

namespace MuseoAurora.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
    }
}
