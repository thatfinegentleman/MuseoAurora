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

        private IEnumerable<Exhibition>? exhibitions;

        protected override async Task OnInitializedAsync()
        {
            exhibitions = await ExhibitionService.GetAllAsync();
        }
    }
}