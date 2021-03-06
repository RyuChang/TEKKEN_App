using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using TekkenApp.Models;
using NewTekkenApp.Utilities;

namespace NewTekkenApp.Pages.StateGroups
{
    public partial class BasePageComponent : NewTekkenApp.Pages.Components.Base.BaseDataComponent<StateGroup, StateGroup_name>
    {
        [Inject]
        protected StateGroupService<StateGroup, StateGroup_name> commonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.StateGroups);
        }

    }
}
