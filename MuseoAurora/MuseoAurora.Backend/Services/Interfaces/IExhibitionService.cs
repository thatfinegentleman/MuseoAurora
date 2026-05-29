using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services.Interfaces
{
    public interface IExhibitionService
    {
        Task<IEnumerable<Exhibition>> GetExhibitionsAsync();
        Task<Exhibition?> GetExhibitionByIdAsync(int id);
        Task<Exhibition?> CreateExhibitionAsync(Exhibition exhibition);
        Task<bool> UpdateExhibitionAsync(Exhibition exhibition);
        Task<bool> DeleteExhibitionAsync(int id);
    }
}
