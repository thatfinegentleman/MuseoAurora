using System.Net.Http.Json;
using MuseoAurora.Models;

namespace MuseoAurora.Client.Services
{
    public class GuidedTourProxyService
    {
        private readonly HttpClient _http;

        public GuidedTourProxyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<GuidedTour>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<GuidedTour>>("api/guidedtours") ?? new();

        public async Task<GuidedTour?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<GuidedTour>($"api/guidedtours/{id}");

        public async Task<bool> CreateAsync(GuidedTour item)
        {
            var response = await _http.PostAsJsonAsync("api/guidedtours", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(GuidedTour item)
        {
            var response = await _http.PutAsJsonAsync("api/guidedtours", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/guidedtours/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}