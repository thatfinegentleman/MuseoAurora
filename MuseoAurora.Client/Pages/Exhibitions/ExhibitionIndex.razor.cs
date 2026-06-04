using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Exhibitions
{
    public partial class ExhibitionIndex : ComponentBase
    {
        [Inject] public ExhibitionProxyService ExhibitionService { get; set; } = default!;
        [CascadingParameter(Name = "AdminState")] public bool IsAdmin { get; set; }

        private IEnumerable<Exhibition>? exhibitions;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            exhibitions = await ExhibitionService.GetAllAsync();
        }

        private async Task Delete(int id)
        {
            await ExhibitionService.DeleteAsync(id);
            await LoadData();
        }
    }
}