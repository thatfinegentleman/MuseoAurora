using MuseoAurora.Models;
using Dapper;
using Npgsql;
using MuseoAurora.Backend.Services.Interfaces;

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

        public async Task<Artwork?> CreateArtworkAsync(Artwork artwork)
        {
            _logger.LogInformation("Creating artwork: {Title}", artwork.Title);

            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                INSERT INTO artworks (
                    "ExhibitionId", 
                    "Title", 
                    "Author", 
                    "Year", 
                    "Description", 
                    "Technique", 
                    "ImageUrl"
                )
                VALUES (
                    @ExhibitionId, 
                    @Title, 
                    @Author, 
                    @Year, 
                    @Description, 
                    @Technique, 
                    @ImageUrl
                )
                RETURNING "Id"; 
                """;

            try
            {
                var newId = await connection.ExecuteScalarAsync<int>(query, artwork);
                artwork.Id = newId;
                return artwork;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating {Title}", artwork.Title);
                return null;
            }
        }

        public async Task<bool> UpdateArtworkAsync(Artwork artwork)
        {
            _logger.LogInformation("Updating artwork of ID: {Id}", artwork.Id);

            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                UPDATE Artworks
                SET 
                    "ExhibitionId" = @ExhibitionId,
                    "Title" = @Title,
                    "Author" = @Author,
                    "Year" = @Year,
                    "Description" = @Description,
                    "Technique" = @Technique,
                    "ImageUrl" = @ImageUrl
                WHERE "Id" = @Id;
                """;

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, artwork);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating ID {Id}", artwork.Id);
                return false;
            }
        }

        public async Task<Artwork?> GetArtworkByIdAsync(int id)
        {
            _logger.LogInformation("Looking for {Id}", id);

            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                SELECT 
                    "Id", 
                    "ExhibitionId",
                    "Title", 
                    "Author",
                    "Year",
                    "Description", 
                    "Technique",
                    "ImageUrl"
                FROM Artworks
                WHERE "Id" = @Id;
                """;

            return await connection.QueryFirstOrDefaultAsync<Artwork>(query, new { Id = id });
        }

        public async Task<IEnumerable<Artwork>> GetArtworksAsync()
        {
            _logger.LogInformation("Loading artworks...");

            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                SELECT 
                    "Id", 
                    "ExhibitionId",
                    "Title", 
                    "Author",
                    "Year",
                    "Description", 
                    "Technique",
                    "ImageUrl"
                FROM Artworks;
                """;

            return await connection.QueryAsync<Artwork>(query);
        }

        public async Task<bool> DeleteArtworkAsync(int id)
        {
            _logger.LogInformation("Deleting artwork with ID: {Id}", id);

            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                DELETE FROM Artworks
                WHERE "Id" = @Id;
                """;

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting artwork with ID {Id}", id);
                return false;
            }
        }
    }
}
