using MuseoAurora.Models;
using System.Net.Http.Json;

namespace MuseoAurora.Frontend.Services
{
    public class ArtworkClient
    {
        private readonly HttpClient _httpClient;

        public ArtworkClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Artwork>?> GetArtworksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Artwork>>("api/artworks");
        }

        public async Task<Artwork?> GetArtworkByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Artwork>($"api/artworks/{id}");
        }

        public async Task CreateArtworkAsync(Artwork artwork)
        {
            await _httpClient.PostAsJsonAsync("api/artworks", artwork);
        }

        public async Task UpdateArtworkAsync(Artwork artwork)
        {
            await _httpClient.PutAsJsonAsync($"api/artworks/{artwork.Id}", artwork);
        }

        public async Task DeleteArtworkAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/artworks/{id}");
        }
    }
}
