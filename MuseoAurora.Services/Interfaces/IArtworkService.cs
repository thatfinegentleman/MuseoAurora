using System.Collections.Generic;
using System.Threading.Tasks;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public interface IArtworkService
    {
        Task<IEnumerable<Artwork>> GetArtworksAsync();
        Task<Artwork?> GetArtworkByIdAsync(int id);
        Task<InsertResult<Artwork>> CreateArtworkAsync(Artwork artwork);
        Task<bool> UpdateArtworkAsync(Artwork artwork);
        Task<bool> DeleteArtworkAsync(int id);
    }
}