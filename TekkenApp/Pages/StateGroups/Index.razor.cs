using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

//using 

namespace TekkenApp.Pages.StateGroups
{
    public partial class Index
    {
        private string title;
        public string Title1 { get; set; } = "서비스";

        [Inject]
        private StateGroupService stateGroupService { get; set; }

        public IList<StateGroup> stateGroups { get; set; }

        protected override async Task OnInitializedAsync()
        {
            stateGroups = await stateGroupService.GetStateGroups();
        }
    }
}
