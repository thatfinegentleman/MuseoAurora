using System;
using System.Collections.Generic;
using System.Text;

namespace MuseoAurora.Models
{
    public class Reservations
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public int GuidedTourId { get; set; }
        public int ParticipantsCount { get; set; }
        public TimeSpan ReservationDate { get; set; }
        public string Status { get; set; }
    }
}
