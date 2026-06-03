using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public class ArtworkService : IArtworkService
    {
        private readonly string _connectionString;

        public ArtworkService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }

        public async Task<IEnumerable<Artwork>> GetArtworksAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = """
                SELECT 
                a.id, 
                a.title, 
                a.author, 
                a.year, 
                a.description, 
                a.technique, 
                a.image_url as ImageUrl, 
                a.exhibition_id, 
                e.id,
                e.title, 
                e.description, 
                e.start_date as StartDate, 
                e.end_date as EndDate, 
                e.image_url as ImageUrl, 
                e.status 
                FROM artworks a LEFT JOIN exhibitions e ON a.exhibition_id = e.id
                """;

            return await connection.QueryAsync<Artwork, Exhibition, Artwork>(
                query,
                (artwork, exhibition) =>
                {
                    if (exhibition != null) artwork.Exhibition = exhibition;
                    return artwork;
                },
                splitOn: "id"
            );
        }

        public async Task<Artwork?> GetArtworkByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                SELECT id, title, author, year, description, technique, image_url as ImageUrl, exhibition_id 
                FROM artworks WHERE id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Artwork>(query, new { Id = id });
        }

        public async Task<InsertResult<Artwork>> CreateArtworkAsync(Artwork artwork)
        {
            var result = new InsertResult<Artwork>();
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    INSERT INTO artworks (title, author, year, description, technique, image_url, exhibition_id)
                    VALUES (@Title, @Author, @Year, @Description, @Technique, @ImageUrl, @ExhibitionId)
                    RETURNING id;";

                var parameters = new
                {
                    artwork.Title,
                    artwork.Author,
                    artwork.Year,
                    artwork.Description,
                    artwork.Technique,
                    artwork.ImageUrl,
                    ExhibitionId = artwork.Exhibition?.Id > 0 ? artwork.Exhibition.Id : (int?)null
                };

                artwork.Id = await connection.ExecuteScalarAsync<int>(query, parameters);
                result.Data = artwork;
            }
            catch (NpgsqlException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<bool> UpdateArtworkAsync(Artwork artwork)
        {
            var status = true;
            try
            {
                    using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    UPDATE artworks 
                    SET title = @Title, author = @Author, year = @Year, description = @Description, 
                        technique = @Technique, image_url = @ImageUrl, exhibition_id = @ExhibitionId
                    WHERE id = @Id";

                var parameters = new
                {
                    artwork.Id,
                    artwork.Title,
                    artwork.Author,
                    artwork.Year,
                    artwork.Description,
                    artwork.Technique,
                    artwork.ImageUrl,
                    ExhibitionId = artwork.Exhibition?.Id > 0 ? artwork.Exhibition.Id : (int?)null
                };

                status = await connection.ExecuteAsync(query, parameters) > 0;
            }
            catch (NpgsqlException ex)
            {
                return false;
            }
            return status;
        }

        public async Task<bool> DeleteArtworkAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE FROM artworks WHERE id = @Id", new { Id = id }) > 0;
        }
    }
}