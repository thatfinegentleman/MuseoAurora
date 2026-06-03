using System.Net.Http.Json;
using MuseoAurora.Models;

namespace MuseoAurora.Client.Services
{
    public class TicketProxyService
    {
        private readonly HttpClient _http;

        public TicketProxyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Ticket>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<Ticket>>("api/tickets") ?? new();

        public async Task<Ticket?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Ticket>($"api/tickets/{id}");

        public async Task<bool> CreateAsync(Ticket item)
        {
            var response = await _http.PostAsJsonAsync("api/tickets", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Ticket item)
        {
            var response = await _http.PutAsJsonAsync("api/tickets", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/tickets/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}