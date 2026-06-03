using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.TicketTypes
{
    public partial class TicketTypeForm : ComponentBase
    {
        [Inject] public TicketTypeProxyService TicketTypeService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;

        [Parameter] public int? Id { get; set; }

        private TicketType ticketType = new TicketType();

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var existing = await TicketTypeService.GetByIdAsync(Id.Value);
                if (existing != null)
                {
                    ticketType = existing;
                }
            }
        }

        private async Task HandleSubmit()
        {
            bool success;
            if (Id.HasValue)
            {
                success = await TicketTypeService.UpdateAsync(ticketType);
            }
            else
            {
                success = await TicketTypeService.CreateAsync(ticketType);
            }

            if (success)
            {
                NavManager.NavigateTo("/tickettypes");
            }
        }
    }
}