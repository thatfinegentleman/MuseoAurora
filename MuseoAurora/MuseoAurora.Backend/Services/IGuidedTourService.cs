namespace MuseoAurora.Backend.Services
{
    public interface IGuidedTourService
    {
        Task<bool> CheckAvailabilityAsync(int tourId, int partecipanti);
    }
}
