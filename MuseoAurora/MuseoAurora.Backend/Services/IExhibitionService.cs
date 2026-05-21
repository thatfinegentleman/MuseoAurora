using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services
{
    public interface IExhibitionService
    {
        Task<IEnumerable<Exhibition>> GetAllExhibitionsAsync();
        Task<bool> CreateExhibitionAsync(Exhibition exhibition);
    }
}
