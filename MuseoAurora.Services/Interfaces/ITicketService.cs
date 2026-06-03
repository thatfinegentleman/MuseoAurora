using System.Collections.Generic;
using System.Threading.Tasks;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<InsertResult<Ticket>> CreateTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
    }
}