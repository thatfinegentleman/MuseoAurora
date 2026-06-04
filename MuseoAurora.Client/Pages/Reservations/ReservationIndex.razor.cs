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
        [CascadingParameter(Name = "AdminState")] public bool IsAdmin { get; set; }

        private IEnumerable<Reservation>? reservations;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            reservations = await ReservationService.GetAllAsync();
        }

        private async Task Delete(int id)
        {
            await ReservationService.DeleteAsync(id);
            await LoadData();
        }
    }
}