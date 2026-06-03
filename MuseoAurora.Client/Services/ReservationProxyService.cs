using System.Net.Http.Json;
using MuseoAurora.Models;

namespace MuseoAurora.Client.Services
{
    public class ReservationProxyService
    {
        private readonly HttpClient _http;

        public ReservationProxyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Reservation>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<Reservation>>("api/reservations") ?? new();

        public async Task<Reservation?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Reservation>($"api/reservations/{id}");

        public async Task<bool> CreateAsync(Reservation item)
        {
            var response = await _http.PostAsJsonAsync("api/reservations", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Reservation item)
        {
            var response = await _http.PutAsJsonAsync("api/reservations", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/reservations/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}