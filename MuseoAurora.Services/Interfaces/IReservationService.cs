using System.Collections.Generic;
using System.Threading.Tasks;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task<InsertResult<Reservation>> CreateReservationAsync(Reservation reservation);
        Task<bool> UpdateReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationAsync(int id);
    }
}