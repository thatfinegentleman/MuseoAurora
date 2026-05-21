using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MuseoAurora.Models
{
    [Table("tickets")]
    public class Ticket
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public int TicketTypeId { get; set; }
        public int ExhibitionId { get; set; }
        public int GuidedTourId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
