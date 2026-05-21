using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private string _connectionString;
        private ILogger<ExhibitionService> _logger;

        public ExhibitionService(IConfiguration configuration,
                                  ILogger<ExhibitionService> logger)
        {
            _connectionString = configuration.GetConnectionString("AuroraDB")
                ?? throw new Exception("ConnectionString 'AuroraDB' not found.");
            _logger = logger;
        }
        public Task<bool> CreateExhibitionAsync(Exhibition exhibition)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exhibition>> GetAllExhibitionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
