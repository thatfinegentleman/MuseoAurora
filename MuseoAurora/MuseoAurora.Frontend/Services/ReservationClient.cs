using MuseoAurora.Models;
using System.Net.Http.Json;

namespace MuseoAurora.Frontend.Services
{

    public class ReservationClient
    {
        private readonly HttpClient _httpClient;

        public ReservationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Reservation>?> GetReservationsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Reservation>>("api/reservations");
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Reservation>($"api/reservations/{id}");
        }

        public async Task CreateReservationAsync(Reservation reservation)
        {
            await _httpClient.PostAsJsonAsync("api/reservations", reservation);
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            await _httpClient.PutAsJsonAsync($"api/reservations/{reservation.Id}", reservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/reservations/{id}");
        }
    }
}
