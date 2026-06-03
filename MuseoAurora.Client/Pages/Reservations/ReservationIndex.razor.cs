using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Reservations
{
    public partial class ReservationIndex : ComponentBase
    {
        [Inject] public ReservationProxyService ReservationService { get; set; } = default!;

        private IEnumerable<Reservation>? reservations;

        protected override async Task OnInitializedAsync()
        {
            reservations = await ReservationService.GetAllAsync();
        }
    }
}