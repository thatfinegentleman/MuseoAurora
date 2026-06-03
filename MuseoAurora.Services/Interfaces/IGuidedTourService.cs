using System.Collections.Generic;
using System.Threading.Tasks;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public interface IGuidedTourService
    {
        Task<IEnumerable<GuidedTour>> GetGuidedToursAsync();
        Task<GuidedTour?> GetGuidedTourByIdAsync(int id);
        Task<InsertResult<GuidedTour>> CreateGuidedTourAsync(GuidedTour tour);
        Task<bool> UpdateGuidedTourAsync(GuidedTour tour);
        Task<bool> DeleteGuidedTourAsync(int id);
    }
}