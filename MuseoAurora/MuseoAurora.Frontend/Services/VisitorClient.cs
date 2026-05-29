using MuseoAurora.Models;
using System.Net.Http.Json;

namespace MuseoAurora.Frontend.Services
{
    public class VisitorClient
    {
        private readonly HttpClient _httpClient;

        public VisitorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Visitor>?> GetVisitorsAsync() {
            return await _httpClient.GetFromJsonAsync<List<Visitor>>("api/visitors");
        }

        public async Task<Visitor?> GetVisitorByIdAsync(int id) {
            return await _httpClient.GetFromJsonAsync<Visitor>($"api/visitors/{id}");
        }

        public async Task CreateVisitorAsync(Visitor visitor) { 
            await _httpClient.PostAsJsonAsync("api/visitors", visitor);}

        public async Task UpdateVisitorAsync(Visitor visitor) {
            await _httpClient.PutAsJsonAsync($"api/visitors/{visitor.Id}", visitor);
        }

        public async Task DeleteVisitorAsync(int id) {
            await _httpClient.DeleteAsync($"api/visitors/{id}");
        }
    }
}
