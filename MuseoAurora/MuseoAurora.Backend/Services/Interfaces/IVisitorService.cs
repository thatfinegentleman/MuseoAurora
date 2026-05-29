using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services.Interfaces
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetVisitorsAsync();
        Task<Visitor?> GetVisitorByIdAsync(int id);
        Task<Visitor?> CreateVisitorAsync(Visitor visitor);
        Task<bool> UpdateVisitorAsync(Visitor visitor);
        Task<bool> DeleteVisitorAsync(int id);
    }
}
