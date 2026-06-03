using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly string _connectionString;

        public VisitorService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }

        public async Task<IEnumerable<Visitor>> GetVisitorsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<Visitor>("SELECT * FROM visitors");
        }

        public async Task<Visitor?> GetVisitorByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Visitor>("SELECT * FROM visitors WHERE id = @Id", new { Id = id });
        }

        public async Task<InsertResult<Visitor>> CreateVisitorAsync(Visitor visitor)
        {
            var result = new InsertResult<Visitor>();
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    INSERT INTO visitors (first_name, last_name, email)
                    VALUES (@FirstName, @LastName, @Email)
                    RETURNING id;";

                visitor.Id = await connection.ExecuteScalarAsync<int>(query, visitor);
                result.Data = visitor;
            }
            catch (NpgsqlException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<bool> UpdateVisitorAsync(Visitor visitor)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE visitors 
                SET first_name = @FirstName, last_name = @LastName, email = @Email
                WHERE id = @Id";
            return await connection.ExecuteAsync(query, visitor) > 0;
        }

        public async Task<bool> DeleteVisitorAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE FROM visitors WHERE id = @Id", new { Id = id }) > 0;
        }
    }
}