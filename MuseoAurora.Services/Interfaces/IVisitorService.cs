using System.Collections.Generic;
using System.Threading.Tasks;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetVisitorsAsync();
        Task<Visitor?> GetVisitorByIdAsync(int id);
        Task<InsertResult<Visitor>> CreateVisitorAsync(Visitor visitor);
        Task<bool> UpdateVisitorAsync(Visitor visitor);
        Task<bool> DeleteVisitorAsync(int id);
    }
}