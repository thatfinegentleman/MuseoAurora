using System;
using System.Collections.Generic;
using System.Text;

namespace MuseoAurora.Models
{
    public class GuidedTours
    {
        public int Id { get; set; }
        public int ExhibitionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public string GuideName { get; set; }
        public int MaxParticipants { get; set; }
    }
}
