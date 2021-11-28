using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;
using TekkenApp.Utilities;

namespace TekkenApp.Pages.StateGroups
{
    public partial class BaseDataComponent
    {
        protected AppType appType = AppType.StateGroups;
        
        [Parameter]
        public int Id { get; set; }

        [Inject]
        protected StateGroupService commonService { get; set; }

        [Inject]
        protected NavigationUtil navigationUtil { get; set; }
    }
}
