using System.Net.Http.Json;
using MuseoAurora.Models;

namespace MuseoAurora.Client.Services
{
    public class VisitorProxyService
    {
        private readonly HttpClient _http;

        public VisitorProxyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Visitor>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<Visitor>>("api/visitors") ?? new();

        public async Task<Visitor?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Visitor>($"api/visitors/{id}");

        public async Task<bool> CreateAsync(Visitor item)
        {
            var response = await _http.PostAsJsonAsync("api/visitors", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Visitor item)
        {
            var response = await _http.PutAsJsonAsync("api/visitors", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/visitors/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}