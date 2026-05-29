using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services.Interfaces
{
    public interface ITicketService
    {
        Task<bool> PurchaseTicketAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> GetTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<Ticket?> CreateTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
    }
}
