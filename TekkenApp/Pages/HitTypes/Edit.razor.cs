using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Edit
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private HitTypeService hitTypeService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        public HitType hitType;

        protected override async Task OnInitializedAsync()
        {
            
            hitType = await hitTypeService.GetEntityByIdAsync(Id);
        }


        protected async Task btnEdit_Click()
        {
            await hitTypeService.UpdateHitTypeAsync(hitType);
            navigationManager.NavigateTo($"/HitTypes/Details/{Id}");
        }

        protected void btnCancel_click()
        {
            navigationManager.NavigateTo("/Videos");
        }
    }
}
