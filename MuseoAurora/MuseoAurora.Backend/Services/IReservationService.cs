using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services
{
    public interface IReservationService
    {
        Task<bool> CreateReservationAsync(Reservation reservation);
    }
}
