using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Details_name
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private HitTypeService hitTypeService { get; set; }

        //public HitType_name hitType_name = new HitType_name();

        [Inject]
        NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //hitTypes = await hitTypeService.GetHitTypes();
            //baseService = hitTypeService;
        }

        protected async Task btnEdit_Click()
        {
            //await hitTypeService.UpdateHitTypeNameAsync(hitType_name);
            //navigationManager.NavigateTo($"/HitTypes/Details_name/{Id}");
        }

    }
}
