using Dapper;
using Microsoft.Extensions.Configuration;
using MuseoAurora.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace MuseoAurora.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly string _connectionString;

        public ExhibitionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }

        public async Task<IEnumerable<Exhibition>> GetExhibitionsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = "SELECT id, title, description, start_date as StartDate, end_date as EndDate, image_url as ImageUrl, status FROM exhibitions";
            return await connection.QueryAsync<Exhibition>(query);
        }

        public async Task<Exhibition?> GetExhibitionByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = "SELECT id, title, description, start_date as StartDate, end_date as EndDate, image_url as ImageUrl, status FROM exhibitions WHERE id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Exhibition>(query, new { Id = id });
        }

        public async Task<InsertResult<Exhibition>> CreateExhibitionAsync(Exhibition exhibition)
        {
            var result = new InsertResult<Exhibition>();
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    INSERT INTO exhibitions (title, description, start_date, end_date, image_url, status)
                    VALUES (@Title, @Description, @StartDate, @EndDate, @ImageUrl, @Status)
                    RETURNING id;";

                exhibition.Id = await connection.ExecuteScalarAsync<int>(query, exhibition);
                result.Data = exhibition;
            }
            catch (NpgsqlException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<bool> UpdateExhibitionAsync(Exhibition exhibition)
        {
            var status = true;
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                UPDATE exhibitions 
                SET title = @Title, description = @Description, start_date = @StartDate, 
                    end_date = @EndDate, image_url = @ImageUrl, status = @Status
                WHERE id = @Id";
                status = await connection.ExecuteAsync(query, exhibition) > 0;
            }
            catch (NpgsqlException ex)
            {
                return false;
            }
            return status;
        }

        public async Task<bool> DeleteExhibitionAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE FROM exhibitions WHERE id = @Id", new { Id = id }) > 0;
        }
    }
}