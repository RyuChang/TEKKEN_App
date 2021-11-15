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

        public IList<HitType> hitTypes { get; set; }
        //public IList<HitType_name> hitType_names { get; set; }

        protected override async Task OnInitializedAsync()
        {
            hitTypes = await hitTypeService.GetHitTypes();
        }
    }
}
