using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MuseoAurora.Models
{
    [Table("reservations")]
    public class Reservation
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public int GuidedTourId { get; set; }
        public int ParticipantsCount { get; set; }
        public TimeSpan ReservationDate { get; set; }
        public string Status { get; set; }
    }
}
