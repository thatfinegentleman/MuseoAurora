using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Tickets
{
    public partial class TicketIndex : ComponentBase
    {
        [Inject] public TicketProxyService TicketService { get; set; } = default!;

        private IEnumerable<Ticket>? tickets;

        protected override async Task OnInitializedAsync()
        {
            tickets = await TicketService.GetAllAsync();
        }
    }
}