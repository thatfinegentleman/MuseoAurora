using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public class GuidedTourService : IGuidedTourService
    {
        private readonly string _connectionString;

        public GuidedTourService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }

        public async Task<IEnumerable<GuidedTour>> GetGuidedToursAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                SELECT gt.id, gt.title, gt.description, gt.start_time as StartTime, gt.duration_minutes as DurationMinutes, gt.guide_name as GuideName, gt.max_participants as MaxParticipants, gt.exhibition_id,
                       e.id, e.title, e.description, e.start_date as StartDate, e.end_date as EndDate, e.image_url as ImageUrl, e.status
                FROM guided_tours gt
                INNER JOIN exhibitions e ON gt.exhibition_id = e.id";

            return await connection.QueryAsync<GuidedTour, Exhibition, GuidedTour>(
                query,
                (tour, exhibition) =>
                {
                    tour.Exhibition = exhibition;
                    return tour;
                },
                splitOn: "id"
            );
        }

        public async Task<GuidedTour?> GetGuidedTourByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = """
                    SELECT    
                    gt.id, 
                    gt.title, 
                    gt.description, 
                    gt.start_time as StartTime, 
                    gt.duration_minutes as DurationMinutes, 
                    gt.guide_name as GuideName, 
                    gt.max_participants as MaxParticipants, 
                    gt.exhibition_id, 
                    e.id, 
                    e.title, 
                    e.description,
                    e.start_date as StartDate, 
                    e.end_date as EndDate, 
                    e.image_url as ImageUrl, 
                    e.status 
                    FROM guided_tours gt INNER JOIN exhibitions e ON gt.exhibition_id = e.id
                    WHERE id = @Id
                    """;
            return await connection.QueryFirstOrDefaultAsync<GuidedTour>(query, new { Id = id });
        }

        public async Task<InsertResult<GuidedTour>> CreateGuidedTourAsync(GuidedTour tour)
        {
            var result = new InsertResult<GuidedTour>();
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = """
                    SELECT    
                    gt.id, 
                    gt.title, 
                    gt.description, 
                    gt.start_time, 
                    gt.duration_minutes, 
                    gt.guide_name, 
                    gt.max_participants, 
                    gt.exhibition_id, 
                    e.id, 
                    e.title, 
                    e.description,
                    e.start_date, 
                    e.end_date, 
                    e.image_url, 
                    e.status 
                    FROM guided_tours gt INNER JOIN exhibitions e ON gt.exhibition_id = e.id
                    """;

                var parameters = new
                {
                    tour.Title,
                    tour.Description,
                    tour.StartTime,
                    tour.DurationMinutes,
                    tour.GuideName,
                    tour.MaxParticipants,
                    ExhibitionId = tour.Exhibition?.Id
                };

                tour.Id = await connection.ExecuteScalarAsync<int>(query, parameters);
                result.Data = tour;
            }
            catch (NpgsqlException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<bool> UpdateGuidedTourAsync(GuidedTour tour)
        {
            var status = true;
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    UPDATE guided_tours 
                    SET title = @Title, description = @Description, start_time = @StartTime, 
                        duration_minutes = @DurationMinutes, guide_name = @GuideName, 
                        max_participants = @MaxParticipants, exhibition_id = @ExhibitionId
                    WHERE id = @Id";

                var parameters = new
                {
                    tour.Id,
                    tour.Title,
                    tour.Description,
                    tour.StartTime,
                    tour.DurationMinutes,
                    tour.GuideName,
                    tour.MaxParticipants,
                    ExhibitionId = tour.Exhibition?.Id
                };
                status = await connection.ExecuteAsync(query, parameters) > 0;
            }
            catch (NpgsqlException ex)
            {
                return false;
            }
            return status;
        }

        public async Task<bool> DeleteGuidedTourAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE FROM guided_tours WHERE id = @Id", new { Id = id }) > 0;
        }
    }
}