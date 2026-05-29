using MuseoAurora.Models;
using System.Net.Http.Json;

namespace MuseoAurora.Frontend.Services
{
    public class TicketClient
    {
        private readonly HttpClient _httpClient;

        public TicketClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Ticket>?> GetTicketsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>("api/tickets");
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ticket>($"api/tickets/{id}");
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            await _httpClient.PostAsJsonAsync("api/tickets", ticket);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            await _httpClient.PutAsJsonAsync($"api/tickets/{ticket.Id}", ticket);
        }

        public async Task DeleteTicketAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/tickets/{id}");
        }
    }
}
