using Dapper;
using MuseoAurora.Backend.Services.Interfaces;
using MuseoAurora.Models;
using Npgsql;

namespace MuseoAurora.Backend.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly string _connectionString;
        private readonly ILogger<VisitorService> _logger;

        public VisitorService(IConfiguration configuration, ILogger<VisitorService> logger)
        {
            _connectionString = configuration.GetConnectionString("AuroraDB")
                ?? throw new Exception("ConnectionString 'AuroraDB' not found.");
            _logger = logger;
        }

        public async Task<IEnumerable<Visitor>> GetVisitorsAsync()
        {
            _logger.LogInformation("Loading visitors...");
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                SELECT 
                "Id", 
                "FirstName", 
                "LastName", 
                "Email", 
                "Name", 
                "Price" 
                FROM 
                Visitors;
                """;

            try
            {
                return await connection.QueryAsync<Visitor>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading visitors");
                return Enumerable.Empty<Visitor>();
            }
        }

        public async Task<Visitor?> GetVisitorByIdAsync(int id)
        {
            _logger.LogInformation("Looking for visitor with ID: {Id}", id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                SELECT
                "Id", 
                "FirstName", 
                "LastName", 
                "Email", 
                "Name", 
                "Price" 
                FROM Visitors 
                WHERE "Id" = @Id;
                """;

            try
            {
                return await connection.QueryFirstOrDefaultAsync<Visitor>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching visitor with ID {Id}", id);
                return null;
            }
        }

        public async Task<Visitor?> CreateVisitorAsync(Visitor visitor)
        {
            _logger.LogInformation("Creating a new visitor: {FirstName} {LastName}", visitor.FirstName, visitor.LastName);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                INSERT INTO Visitors
                ( 
                "FirstName", 
                "LastName", 
                "Email", 
                "Name", 
                "Price" )
                VALUES (@FirstName, @LastName, @Email, @Name, @Price)
                RETURNING "Id";
                """;

            try
            {
                var newId = await connection.ExecuteScalarAsync<int>(query, visitor);
                visitor.Id = newId;
                return visitor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating visitor {Email}", visitor.Email);
                return null;
            }
        }

        public async Task<bool> UpdateVisitorAsync(Visitor visitor)
        {
            _logger.LogInformation("Updating visitor with ID: {Id}", visitor.Id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
                UPDATE Visitors
                SET 
                    "FirstName" = @FirstName, 
                    "LastName" = @LastName, 
                    "Email" = @Email, 
                    "Name" = @Name, 
                    "Price" = @Price
                WHERE "Id" = @Id;
                """;

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, visitor);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating visitor with ID {Id}", visitor.Id);
                return false;
            }
        }

        public async Task<bool> DeleteVisitorAsync(int id)
        {
            _logger.LogInformation("Deleting visitor with ID: {Id}", id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """DELETE FROM Visitors WHERE "Id" = @Id;""";

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting visitor with ID {Id}", id);
                return false;
            }
        }
    }
}
