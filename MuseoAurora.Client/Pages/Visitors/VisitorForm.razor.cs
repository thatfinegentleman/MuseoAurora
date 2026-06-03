using Microsoft.AspNetCore.Components;
using MuseoAurora.Models;
using MuseoAurora.Client.Services;
using System.Threading.Tasks;

namespace MuseoAurora.Client.Pages.Visitors
{
    public partial class VisitorForm : ComponentBase
    {
        [Inject] public VisitorProxyService VisitorService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;

        [Parameter] public int? Id { get; set; }

        private Visitor visitor = new Visitor();

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var existing = await VisitorService.GetByIdAsync(Id.Value);
                if (existing != null)
                {
                    visitor = existing;
                }
            }
        }

        private async Task HandleSubmit()
        {
            bool success;
            if (Id.HasValue)
            {
                success = await VisitorService.UpdateAsync(visitor);
            }
            else
            {
                success = await VisitorService.CreateAsync(visitor);
            }

            if (success)
            {
                NavManager.NavigateTo("/visitors");
            }
        }
    }
}