using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Artworks
{
    public partial class ArtworkForm : ComponentBase
    {
        [Inject] public ArtworkProxyService ArtworkService { get; set; } = default!;
        [Inject] public ExhibitionProxyService ExhibitionService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;

        [Parameter] public int? Id { get; set; }

        private Artwork artwork = new Artwork();
        private IEnumerable<Exhibition> availableExhibitions = new List<Exhibition>();

        protected override async Task OnInitializedAsync()
        {
            availableExhibitions = await ExhibitionService.GetAllAsync() ?? new List<Exhibition>();

            if (Id.HasValue)
            {
                var existing = await ArtworkService.GetByIdAsync(Id.Value);
                if (existing != null)
                {
                    artwork = existing;
                }
            }

            artwork.Exhibition ??= new Exhibition();
        }

        private async Task HandleSubmit()
        {
            bool success;
            if (Id.HasValue)
            {
                success = await ArtworkService.UpdateAsync(artwork);
            }
            else
            {
                success = await ArtworkService.CreateAsync(artwork);
            }

            if (success)
            {
                NavManager.NavigateTo("/artworks");
            }
        }
    }
}