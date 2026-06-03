using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly string _connectionString;

        public TicketTypeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }

        public async Task<IEnumerable<TicketType>> GetTicketTypesAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<TicketType>("SELECT * FROM ticket_types");
        }

        public async Task<TicketType?> GetTicketTypeByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<TicketType>("SELECT * FROM ticket_types WHERE id = @Id", new { Id = id });
        }

        public async Task<InsertResult<TicketType>> CreateTicketTypeAsync(TicketType ticketType)
        {
            var result = new InsertResult<TicketType>();
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    INSERT INTO ticket_types (name, price)
                    VALUES (@Name, @Price)
                    RETURNING id;";

                ticketType.Id = await connection.ExecuteScalarAsync<int>(query, ticketType);
                result.Data = ticketType;
            }
            catch (NpgsqlException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<bool> UpdateTicketTypeAsync(TicketType ticketType)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE ticket_types 
                SET name = @Name, price = @Price
                WHERE id = @Id";
            return await connection.ExecuteAsync(query, ticketType) > 0;
        }

        public async Task<bool> DeleteTicketTypeAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE FROM ticket_types WHERE id = @Id", new { Id = id }) > 0;
        }
    }
}