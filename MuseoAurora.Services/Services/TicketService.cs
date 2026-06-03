using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public class TicketService : ITicketService
    {
        private readonly string _connectionString;

        public TicketService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                SELECT t.*, v.*, tt.*, e.*, gt.*
                FROM tickets t
                INNER JOIN visitors v ON t.visitor_id = v.id
                INNER JOIN ticket_types tt ON t.ticket_type_id = tt.id
                LEFT JOIN exhibitions e ON t.exhibition_id = e.id
                LEFT JOIN guided_tours gt ON t.guided_tour_id = gt.id";

            return await connection.QueryAsync<Ticket, Visitor, TicketType, Exhibition, GuidedTour, Ticket>(
                query,
                (ticket, visitor, type, exhibition, tour) =>
                {
                    ticket.Visitor = visitor;
                    ticket.TicketType = type;
                    if (exhibition != null) ticket.Exhibition = exhibition;
                    if (tour != null) ticket.GuidedTour = tour;
                    return ticket;
                },
                splitOn: "id,id,id,id"
            );
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Ticket>("SELECT * FROM tickets WHERE id = @Id", new { Id = id });
        }

        public async Task<InsertResult<Ticket>> CreateTicketAsync(Ticket ticket)
        {
            var result = new InsertResult<Ticket>();
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    INSERT INTO tickets (quantity, total_price, purchase_date, visitor_id, ticket_type_id, exhibition_id, guided_tour_id)
                    VALUES (@Quantity, @TotalPrice, @PurchaseDate, @VisitorId, @TicketTypeId, @ExhibitionId, @GuidedTourId)
                    RETURNING id;";

                var parameters = new
                {
                    ticket.Quantity,
                    ticket.TotalPrice,
                    ticket.PurchaseDate,
                    VisitorId = ticket.Visitor?.Id,
                    TicketTypeId = ticket.TicketType?.Id,
                    ExhibitionId = ticket.Exhibition?.Id > 0 ? ticket.Exhibition.Id : (int?)null,
                    GuidedTourId = ticket.GuidedTour?.Id > 0 ? ticket.GuidedTour.Id : (int?)null
                };

                ticket.Id = await connection.ExecuteScalarAsync<int>(query, parameters);
                result.Data = ticket;
            }
            catch (NpgsqlException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE tickets 
                SET quantity = @Quantity, total_price = @TotalPrice, purchase_date = @PurchaseDate, 
                    visitor_id = @VisitorId, ticket_type_id = @TicketTypeId, 
                    exhibition_id = @ExhibitionId, guided_tour_id = @GuidedTourId
                WHERE id = @Id";

            var parameters = new
            {
                ticket.Id,
                ticket.Quantity,
                ticket.TotalPrice,
                ticket.PurchaseDate,
                VisitorId = ticket.Visitor?.Id,
                TicketTypeId = ticket.TicketType?.Id,
                ExhibitionId = ticket.Exhibition?.Id > 0 ? ticket.Exhibition.Id : (int?)null,
                GuidedTourId = ticket.GuidedTour?.Id > 0 ? ticket.GuidedTour.Id : (int?)null
            };

            return await connection.ExecuteAsync(query, parameters) > 0;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE FROM tickets WHERE id = @Id", new { Id = id }) > 0;
        }
    }
}