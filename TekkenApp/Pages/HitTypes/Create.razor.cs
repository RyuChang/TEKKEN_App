using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Create
    {
        [Inject]
        private HitTypeService hitTypeService { get; set; }
        //[Inject]
        //private BaseService baseService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        //public HitType hitType = new HitType();

        //protected override async Task OnInitializedAsync()
        //{
        //    //hitType.Number = await hitTypeService.GetCreateNumber();
        //}

        //protected async void btnSave_Click()
        //{
        //    //await hitTypeService.CreateHitTypeAsync(hitType);
        //    navigationManager.NavigateTo("/HitTypes/");
        //}
    }
}

