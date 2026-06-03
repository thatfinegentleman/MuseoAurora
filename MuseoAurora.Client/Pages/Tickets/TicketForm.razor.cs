using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Tickets
{
    public partial class TicketForm : ComponentBase
    {
        [Inject] public TicketProxyService TicketService { get; set; } = default!;
        [Inject] public VisitorProxyService VisitorService { get; set; } = default!;
        [Inject] public TicketTypeProxyService TicketTypeService { get; set; } = default!;
        [Inject] public ExhibitionProxyService ExhibitionService { get; set; } = default!;
        [Inject] public GuidedTourProxyService GuidedTourService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;

        [Parameter] public int? Id { get; set; }

        private Ticket ticket = new Ticket();
        private IEnumerable<Visitor> availableVisitors = new List<Visitor>();
        private IEnumerable<TicketType> availableTicketTypes = new List<TicketType>();
        private IEnumerable<Exhibition> availableExhibitions = new List<Exhibition>();
        private IEnumerable<GuidedTour> availableTours = new List<GuidedTour>();

        protected override async Task OnInitializedAsync()
        {
            availableVisitors = await VisitorService.GetAllAsync() ?? new List<Visitor>();
            availableTicketTypes = await TicketTypeService.GetAllAsync() ?? new List<TicketType>();
            availableExhibitions = await ExhibitionService.GetAllAsync() ?? new List<Exhibition>();
            availableTours = await GuidedTourService.GetAllAsync() ?? new List<GuidedTour>();

            if (Id.HasValue)
            {
                var existing = await TicketService.GetByIdAsync(Id.Value);
                if (existing != null)
                {
                    ticket = existing;
                }
            }

            ticket.Visitor ??= new Visitor();
            ticket.TicketType ??= new TicketType();
            ticket.Exhibition ??= new Exhibition();
            ticket.GuidedTour ??= new GuidedTour();
        }

        private async Task HandleSubmit()
        {
            bool success;
            if (Id.HasValue)
            {
                success = await TicketService.UpdateAsync(ticket);
            }
            else
            {
                success = await TicketService.CreateAsync(ticket);
            }

            if (success)
            {
                NavManager.NavigateTo("/tickets");
            }
        }
    }
}