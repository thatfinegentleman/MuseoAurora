using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Reservations
{
    public partial class ReservationForm : ComponentBase
    {
        [Inject] public ReservationProxyService ReservationService { get; set; } = default!;
        [Inject] public VisitorProxyService VisitorService { get; set; } = default!;
        [Inject] public GuidedTourProxyService GuidedTourService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;
        [CascadingParameter(Name = "AdminState")] public bool IsAdmin { get; set; }

        [Parameter] public int? Id { get; set; }

        private Reservation reservation = new Reservation();
        private IEnumerable<Visitor> availableVisitors = new List<Visitor>();
        private IEnumerable<GuidedTour> availableTours = new List<GuidedTour>();

        protected override async Task OnInitializedAsync()
        {
            availableVisitors = await VisitorService.GetAllAsync() ?? new List<Visitor>();
            availableTours = await GuidedTourService.GetAllAsync() ?? new List<GuidedTour>();

            if (Id.HasValue)
            {
                var existing = await ReservationService.GetByIdAsync(Id.Value);
                if (existing != null)
                {
                    reservation = existing;
                }
            }

            reservation.Visitor ??= new Visitor();
            reservation.GuidedTour ??= new GuidedTour();
        }

        private async Task HandleSubmit()
        {
            bool success;
            if (Id.HasValue)
            {
                success = await ReservationService.UpdateAsync(reservation);
            }
            else
            {
                success = await ReservationService.CreateAsync(reservation);
            }

            if (success)
            {
                NavManager.NavigateTo("/reservations");
            }
        }
    }
}