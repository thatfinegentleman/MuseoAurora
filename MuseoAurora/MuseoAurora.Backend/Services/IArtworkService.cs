using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services
{
    public interface IArtworkService
    {
        Task<IEnumerable<Artwork>> GetArtworksAsync();
        Task<Artwork?> GetArtworkByIdAsync(int id);
        Task<Artwork?> CreateArtworkAsync(Artwork artwork);
        Task<bool> UpdateArtworkAsync(Artwork artwork);
        Task<bool> DeleteArtworkAsync(int id);
    }
}
