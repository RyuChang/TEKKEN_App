using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Edit_name
    {
        [Parameter]
        public string Id { get; set; }

        public HitType_name hitType_name = new HitType_name();

        [Inject]
        private HitTypeService hitTypeService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        
        protected override async Task OnInitializedAsync()
        {
            hitType_name = await hitTypeService.GetNameEntityByIdAsync(Id);
        }


        //protected async Task btnEdit_Click()
        //{
        //    await hitTypeService.UpdateHitTypeNameAsync(hitType_name);
        //    navigationManager.NavigateTo($"/HitTypes/Details_name/{Id}");
        //}

        protected void btnCancel_click()
        {
            navigationManager.NavigateTo("/Videos");
        }
    }
}
