using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services
{
    public interface IArtworkService
    {
        Task<Artwork> GetArtworksAsync();
        Task<Artwork> GetArtworkByIdAsync();
    }
}
