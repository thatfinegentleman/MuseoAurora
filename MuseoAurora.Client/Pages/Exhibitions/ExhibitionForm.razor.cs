using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Exhibitions
{
    public partial class ExhibitionForm : ComponentBase
    {
        [Inject] public ExhibitionProxyService ExhibitionService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;

        [Parameter] public int? Id { get; set; }

        private Exhibition exhibition = new Exhibition();

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var existing = await ExhibitionService.GetByIdAsync(Id.Value);
                if (existing != null)
                {
                    exhibition = existing;
                }
            }
        }

        private async Task HandleSubmit()
        {
            bool success;
            if (Id.HasValue)
            {
                success = await ExhibitionService.UpdateAsync(exhibition);
            }
            else
            {
                success = await ExhibitionService.CreateAsync(exhibition);
            }

            if (success)
            {
                NavManager.NavigateTo("/exhibitions");
            }
        }
    }
}