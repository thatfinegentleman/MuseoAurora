using Dapper;
using MuseoAurora.Backend.Services.Interfaces;
using MuseoAurora.Models;
using Npgsql;

namespace MuseoAurora.Backend.Services
{
    public class ReservationService : IReservationService
    {
        private readonly string _connectionString;
        private readonly ILogger<ReservationService> _logger;

        public ReservationService(IConfiguration configuration, ILogger<ReservationService> logger)
        {
            _connectionString = configuration.GetConnectionString("AuroraDB")
                ?? throw new Exception("ConnectionString 'AuroraDB' not found.");
            _logger = logger;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            _logger.LogInformation("Loading reservations...");
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
            SELECT 
                "Id", 
                "VisitorId", 
                "GuidedTourId", 
                "ParticipantsCount", 
                "ReservationDate", 
                "Status" 
            FROM Reservations;
            """;

            try
            {
                return await connection.QueryAsync<Reservation>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading reservations");
                return Enumerable.Empty<Reservation>();
            }
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            _logger.LogInformation("Looking for reservation with ID: {Id}", id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
            SELECT 
                "Id", 
                "VisitorId", 
                "GuidedTourId", 
                "ParticipantsCount", 
                "ReservationDate", 
                "Status" 
            FROM Reservations 
            WHERE "Id" = @Id;
            """;

            try
            {
                return await connection.QueryFirstOrDefaultAsync<Reservation>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching reservation with ID {Id}", id);
                return null;
            }
        }

        public async Task<Reservation?> CreateReservationAsync(Reservation reservation)
        {
            _logger.LogInformation("Creating a new reservation for Visitor ID: {VisitorId}", reservation.VisitorId);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
            INSERT INTO Reservations
            (
                "VisitorId", 
                "GuidedTourId", 
                "ParticipantsCount", 
                "ReservationDate", 
                "Status"
            )
            VALUES (@VisitorId, @GuidedTourId, @ParticipantsCount, @ReservationDate, @Status)
            RETURNING "Id";
            """;

            try
            {
                var newId = await connection.ExecuteScalarAsync<int>(query, reservation);
                reservation.Id = newId;
                return reservation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating reservation for Visitor ID {VisitorId}", reservation.VisitorId);
                return null;
            }
        }

        public async Task<bool> UpdateReservationAsync(Reservation reservation)
        {
            _logger.LogInformation("Updating reservation with ID: {Id}", reservation.Id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """
            UPDATE Reservations
            SET 
                "VisitorId" = @VisitorId, 
                "GuidedTourId" = @GuidedTourId, 
                "ParticipantsCount" = @ParticipantsCount, 
                "ReservationDate" = @ReservationDate, 
                "Status" = @Status
            WHERE "Id" = @Id;
            """;

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, reservation);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating reservation with ID {Id}", reservation.Id);
                return false;
            }
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            _logger.LogInformation("Deleting reservation with ID: {Id}", id);
            using var connection = new NpgsqlConnection(_connectionString);

            const string query = """DELETE FROM Reservations WHERE "Id" = @Id;""";

            try
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting reservation with ID {Id}", id);
                return false;
            }
        }
    }
}
