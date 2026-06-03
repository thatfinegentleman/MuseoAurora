using System.Net.Http.Json;
using MuseoAurora.Models;

namespace MuseoAurora.Client.Services
{
    public class TicketTypeProxyService
    {
        private readonly HttpClient _http;

        public TicketTypeProxyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TicketType>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<TicketType>>("api/tickettypes") ?? new();

        public async Task<TicketType?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<TicketType>($"api/tickettypes/{id}");

        public async Task<bool> CreateAsync(TicketType item)
        {
            var response = await _http.PostAsJsonAsync("api/tickettypes", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(TicketType item)
        {
            var response = await _http.PutAsJsonAsync("api/tickettypes", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/tickettypes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}