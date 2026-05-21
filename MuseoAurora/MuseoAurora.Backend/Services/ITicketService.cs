using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services
{
    public interface ITicketService
    {
        Task<bool> PurchaseTicketAsync(Ticket ticket);
    }
}
