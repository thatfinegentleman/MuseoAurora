using Microsoft.AspNetCore.Components;
using MuseoAurora.Client.Services;
using MuseoAurora.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MuseoAurora.Client.Pages.Artworks
{
    public partial class ArtworkIndex : ComponentBase
    {
        [Inject] public ArtworkProxyService ArtworkService { get; set; } = default!;
        [CascadingParameter(Name = "AdminState")] public bool IsAdmin { get; set; }

        private IEnumerable<Artwork>? artworks;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            artworks = await ArtworkService.GetAllAsync();
        }

        private async Task Delete(int id)
        {
            await ArtworkService.DeleteAsync(id);
            await LoadData();
        }
    }
}