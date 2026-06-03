using System.Collections.Generic;
using System.Threading.Tasks;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public interface IExhibitionService
    {
        Task<IEnumerable<Exhibition>> GetExhibitionsAsync();
        Task<Exhibition?> GetExhibitionByIdAsync(int id);
        Task<InsertResult<Exhibition>> CreateExhibitionAsync(Exhibition exhibition);
        Task<bool> UpdateExhibitionAsync(Exhibition exhibition);
        Task<bool> DeleteExhibitionAsync(int id);
    }
}