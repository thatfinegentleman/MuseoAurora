using MuseoAurora.Models;

namespace MuseoAurora.Backend.Services
{
    public class ArtworkService : IArtworkService
    {
        private string _connectionString;
        private ILogger<ArtworkService> _logger;

        public ArtworkService(IConfiguration configuration,
                                  ILogger<ArtworkService> logger)
        {
            _connectionString = configuration.GetConnectionString("AuroraDB")
                ?? throw new Exception("ConnectionString 'AuroraDB' not found.");
            _logger = logger;
        }
        public Task<Artwork> GetArtworkByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Artwork> GetArtworksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
