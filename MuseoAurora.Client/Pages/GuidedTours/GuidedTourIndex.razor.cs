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

        private IEnumerable<GuidedTour>? tours;

        protected override async Task OnInitializedAsync()
        {
            tours = await GuidedTourService.GetAllAsync();
        }
    }
}