using MuseoAurora.Models;
using System.Net.Http.Json;

namespace MuseoAurora.Frontend.Services
{
    public class ExhibitionClient
    {
        private readonly HttpClient _httpClient;

        public ExhibitionClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Exhibition>?> GetExhibitionsAsync() {
            return await _httpClient.GetFromJsonAsync<List<Exhibition>>("api/exhibitions");}

        public async Task<Exhibition?> GetExhibitionByIdAsync(int id) {
            return await _httpClient.GetFromJsonAsync<Exhibition>($"api/exhibitions/{id}"); }

        public async Task CreateExhibitionAsync(Exhibition exhibition) {
            await _httpClient.PostAsJsonAsync("api/exhibitions", exhibition);
        }

        public async Task UpdateExhibitionAsync(Exhibition exhibition) {
            await _httpClient.PutAsJsonAsync($"api/exhibitions/{exhibition.Id}", exhibition);}

        public async Task DeleteExhibitionAsync(int id) {
            await _httpClient.DeleteAsync($"api/exhibitions/{id}"); }
    }
}
