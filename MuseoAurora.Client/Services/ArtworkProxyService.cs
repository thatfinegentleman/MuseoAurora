using System.Net.Http.Json;
using MuseoAurora.Models;

namespace MuseoAurora.Client.Services
{
    public class ArtworkProxyService
    {
        private readonly HttpClient _http;

        public ArtworkProxyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Artwork>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<Artwork>>("api/artworks") ?? new();

        public async Task<Artwork?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Artwork>($"api/artworks/{id}");

        public async Task<bool> CreateAsync(Artwork item)
        {
            var response = await _http.PostAsJsonAsync("api/artworks", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Artwork item)
        {
            var response = await _http.PutAsJsonAsync("api/artworks", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/artworks/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}