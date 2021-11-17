using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;


//using 

namespace TekkenApp.Pages.HitTypes
{
    public partial class Index
    {
        private string title;
        public string Title1 { get; set; } = "서비스";

        [Inject]
        private HitTypeService hitTypeService { get; set; }

        NavigationManager navigationManager;

        //public IList<HitType> hitTypes { get; set; }
        //public IList<BaseEntity> baseEntities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //hitTypes = await hitTypeService.GetHitTypes();

            //baseService = hitTypeService;
        }

        protected void btnCreate_Click()
        {
            navigationManager.NavigateTo($"{HitType.PRE_URL}/Create_name");
        }


    }
}
