using Dapper;
using MuseoAurora.Backend.Services.Interfaces;
using MuseoAurora.Models;
using Npgsql;

namespace MuseoAurora.Backend.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly string _connectionString;
        private readonly ILogger<ExhibitionService> _logger;

        public ExhibitionService(IConfiguration configuration, ILogger<ExhibitionService> logger)
        {
            _connectionString = configuration.GetConnectionString("AuroraDB")
                ?? throw new Exception("ConnectionString 'AuroraDB' not found.");
            _logger = logger;
        }

        public async Task<IEnumerable<Exhibition>> GetExhibitionsAsync()
        {
            _logger.LogInformation("Loading exhibitions...");
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                SELECT 
                    "Id",
                    "Title", 
                    "Description", 
                    "StartDate", 
                    "EndDate",
                    "ImageUrl", 
                    "Status" 
                    FROM Exhibitions;
                """;

            try
            {
                return await connection.QueryAsync<Exhibition>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading exhibitions");
                return Enumerable.Empty<Exhibition>();
            }
        }

        public async Task<Exhibition?> GetExhibitionByIdAsync(int id)
        {
            _logger.LogInformation("Looking for exhibition with ID: {Id}", id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                SELECT 
                "Id", 
                "Title",
                "Description", 
                "StartDate", 
                "EndDate", 
                "ImageUrl", 
                "Status" 
                FROM Exhibitions 
                WHERE "Id" = @Id;
                """;

            try
            {
                return await connection.QueryFirstOrDefaultAsync<Exhibition>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching exhibition with ID {Id}", id);
                return null;
            }
        }

        public async Task<Exhibition?> CreateExhibitionAsync(Exhibition exhibition)
        {
            _logger.LogInformation("Creating a new exhibition: {Title}", exhibition.Title);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                INSERT INTO Exhibitions ( 
                "Title",
                "Description", 
                "StartDate", 
                "EndDate", 
                "ImageUrl", 
                "Status")
                VALUES (@Title, @Description, @StartDate, @EndDate, @ImageUrl, @Status)
                RETURNING "Id";
                """;

            try
            {
                var newId = await connection.ExecuteScalarAsync<int>(query, exhibition);
                exhibition.Id = newId;
                return exhibition;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating exhibition {Title}", exhibition.Title);
                return null;
            }
        }

        public async Task<bool> UpdateExhibitionAsync(Exhibition exhibition)
        {
            _logger.LogInformation("Updating exhibition with ID: {Id}", exhibition.Id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                UPDATE Exhibitions
                SET 
                "Title" = @Title, 
                "Description" = @Description, 
                "StartDate" = @StartDate, 
                "EndDate" = @EndDate, 
                "ImageUrl" = @ImageUrl, 
                "Status" = @Status
                WHERE "Id" = @Id;
                """;

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, exhibition);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating exhibition with ID {Id}", exhibition.Id);
                return false;
            }
        }

        public async Task<bool> DeleteExhibitionAsync(int id)
        {
            _logger.LogInformation("Deleting exhibition with ID: {Id}", id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """DELETE FROM Exhibitions WHERE "Id" = @Id;""";

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting exhibition with ID {Id}", id);
                return false;
            }
        }
    }
}
