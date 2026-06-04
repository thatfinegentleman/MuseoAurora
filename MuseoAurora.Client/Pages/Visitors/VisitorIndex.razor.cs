using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Visitors
{
    public partial class VisitorIndex : ComponentBase
    {
        [Inject] public VisitorProxyService VisitorService { get; set; } = default!;

        private IEnumerable<Visitor>? visitors;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            visitors = await VisitorService.GetAllAsync();
        }

        private async Task Delete(int id)
        {
            await VisitorService.DeleteAsync(id);
            await LoadData();
        }
    }
}