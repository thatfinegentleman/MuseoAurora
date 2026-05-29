namespace MuseoAurora.Backend.Services.Interfaces
{
    public interface IGuidedTourService
    {
        Task<bool> CheckAvailabilityAsync(int tourId, int partecipanti);
    }
}
