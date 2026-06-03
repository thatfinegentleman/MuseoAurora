using System.Net.Http.Json;
using MuseoAurora.Models;

namespace MuseoAurora.Client.Services
{
    public class ExhibitionProxyService
    {
        private readonly HttpClient _http;

        public ExhibitionProxyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Exhibition>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<Exhibition>>("api/exhibitions") ?? new();

        public async Task<Exhibition?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Exhibition>($"api/exhibitions/{id}");

        public async Task<bool> CreateAsync(Exhibition item)
        {
            var response = await _http.PostAsJsonAsync("api/exhibitions", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Exhibition item)
        {
            var response = await _http.PutAsJsonAsync("api/exhibitions", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/exhibitions/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}