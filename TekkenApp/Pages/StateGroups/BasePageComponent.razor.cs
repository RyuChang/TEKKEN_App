using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;
using TekkenApp.Utilities;

namespace TekkenApp.Pages.StateGroups
{
    public partial class BasePageComponent : TekkenApp.Pages.Components.Base.BaseDataComponent<StateGroup, StateGroup_name>
    {
        [Inject]
        protected StateGroupService<StateGroup, StateGroup_name> commonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.StateGroups);
        }

    }
}
