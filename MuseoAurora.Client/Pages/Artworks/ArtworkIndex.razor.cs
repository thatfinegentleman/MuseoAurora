using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Artworks
{
    public partial class ArtworkIndex : ComponentBase
    {
        [Inject] public ArtworkProxyService ArtworkService { get; set; } = default!;

        private IEnumerable<Artwork>? artworks;

        protected override async Task OnInitializedAsync()
        {
            artworks = await ArtworkService.GetAllAsync();
        }
    }
}