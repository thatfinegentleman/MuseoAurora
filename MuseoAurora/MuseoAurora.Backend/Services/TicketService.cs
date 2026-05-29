using Dapper;
using MuseoAurora.Backend.Services.Interfaces;
using MuseoAurora.Models;
using Npgsql;

namespace MuseoAurora.Backend.Services
{
    public class TicketService : ITicketService
    {
        private readonly string _connectionString;
        private readonly ILogger<TicketService> _logger;

        public TicketService(IConfiguration configuration, ILogger<TicketService> logger)
        {
            _connectionString = configuration.GetConnectionString("AuroraDB")
                ?? throw new Exception("ConnectionString 'AuroraDB' not found.");
            _logger = logger;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            _logger.LogInformation("Loading tickets...");
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
            SELECT 
                "Id", 
                "VisitorId", 
                "TicketTypeId", 
                "ExhibitionId", 
                "GuidedTourId", 
                "Quantity", 
                "TotalPrice", 
                "PurchaseDate" 
            FROM Tickets;
            """;

            try
            {
                return await connection.QueryAsync<Ticket>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading tickets");
                return Enumerable.Empty<Ticket>();
            }
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            _logger.LogInformation("Looking for ticket with ID: {Id}", id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
            SELECT 
                "Id", 
                "VisitorId", 
                "TicketTypeId", 
                "ExhibitionId", 
                "GuidedTourId", 
                "Quantity", 
                "TotalPrice", 
                "PurchaseDate" 
            FROM Tickets 
            WHERE "Id" = @Id;
            """;

            try
            {
                return await connection.QueryFirstOrDefaultAsync<Ticket>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching ticket with ID {Id}", id);
                return null;
            }
        }

        public async Task<Ticket?> CreateTicketAsync(Ticket ticket)
        {
            _logger.LogInformation("Creating a new ticket for Visitor ID: {VisitorId}", ticket.VisitorId);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
            INSERT INTO Tickets
            (
                "VisitorId", 
                "TicketTypeId", 
                "ExhibitionId", 
                "GuidedTourId", 
                "Quantity", 
                "TotalPrice", 
                "PurchaseDate"
            )
            VALUES (@VisitorId, @TicketTypeId, @ExhibitionId, @GuidedTourId, @Quantity, @TotalPrice, @PurchaseDate)
            RETURNING "Id";
            """;

            try
            {
                var newId = await connection.ExecuteScalarAsync<int>(query, ticket);
                ticket.Id = newId;
                return ticket;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating ticket for Visitor ID {VisitorId}", ticket.VisitorId);
                return null;
            }
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            _logger.LogInformation("Updating ticket with ID: {Id}", ticket.Id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
            UPDATE Tickets
            SET 
                "VisitorId" = @VisitorId, 
                "TicketTypeId" = @TicketTypeId, 
                "ExhibitionId" = @ExhibitionId, 
                "GuidedTourId" = @GuidedTourId, 
                "Quantity" = @Quantity, 
                "TotalPrice" = @TotalPrice, 
                "PurchaseDate" = @PurchaseDate
            WHERE "Id" = @Id;
            """;

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, ticket);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating ticket with ID {Id}", ticket.Id);
                return false;
            }
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            _logger.LogInformation("Deleting ticket with ID: {Id}", id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """DELETE FROM Tickets WHERE "Id" = @Id;""";

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting ticket with ID {Id}", id);
                return false;
            }
        }

        public Task<bool> PurchaseTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
