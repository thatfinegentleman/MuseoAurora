using System.Collections.Generic;
using System.Threading.Tasks;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public interface ITicketTypeService
    {
        Task<IEnumerable<TicketType>> GetTicketTypesAsync();
        Task<TicketType?> GetTicketTypeByIdAsync(int id);
        Task<InsertResult<TicketType>> CreateTicketTypeAsync(TicketType ticketType);
        Task<bool> UpdateTicketTypeAsync(TicketType ticketType);
        Task<bool> DeleteTicketTypeAsync(int id);
    }
}