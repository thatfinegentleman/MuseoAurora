using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.GuidedTours
{
    public partial class GuidedTourForm : ComponentBase
    {
        [Inject] public GuidedTourProxyService GuidedTourService { get; set; } = default!;
        [Inject] public ExhibitionProxyService ExhibitionService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;

        [Parameter] public int? Id { get; set; }

        private GuidedTour tour = new GuidedTour();
        private IEnumerable<Exhibition> availableExhibitions = new List<Exhibition>();

        protected override async Task OnInitializedAsync()
        {
            availableExhibitions = await ExhibitionService.GetAllAsync() ?? new List<Exhibition>();

            if (Id.HasValue)
            {
                var existing = await GuidedTourService.GetByIdAsync(Id.Value);
                if (existing != null)
                {
                    tour = existing;
                }
            }

            tour.Exhibition ??= new Exhibition();
        }

        private async Task HandleSubmit()
        {
            bool success;
            if (Id.HasValue)
            {
                success = await GuidedTourService.UpdateAsync(tour);
            }
            else
            {
                success = await GuidedTourService.CreateAsync(tour);
            }

            if (success)
            {
                NavManager.NavigateTo("/guidedtours");
            }
        }
    }
}