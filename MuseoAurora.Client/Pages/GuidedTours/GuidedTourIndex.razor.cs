using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.GuidedTours
{
    public partial class GuidedTourIndex : ComponentBase
    {
        [Inject] public GuidedTourProxyService GuidedTourService { get; set; } = default!;
        [CascadingParameter(Name = "AdminState")] public bool IsAdmin { get; set; }

        private IEnumerable<GuidedTour>? tours;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            tours = await GuidedTourService.GetAllAsync();
        }

        private async Task Delete(int id)
        {
            await GuidedTourService.DeleteAsync(id);
            await LoadData();
        }
    }
}