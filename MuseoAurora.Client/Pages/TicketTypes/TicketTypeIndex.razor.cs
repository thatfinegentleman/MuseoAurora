using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.TicketTypes
{
    public partial class TicketTypeIndex : ComponentBase
    {
        [Inject] public TicketTypeProxyService TicketTypeService { get; set; } = default!;

        private IEnumerable<TicketType>? ticketTypes;

        protected override async Task OnInitializedAsync()
        {
            ticketTypes = await TicketTypeService.GetAllAsync();
        }
    }
}