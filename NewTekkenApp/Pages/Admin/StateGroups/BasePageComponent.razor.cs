using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Admin.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.StateGroups
{
    public partial class BasePageComponent : BaseDataComponent<StateGroup, StateGroup_name>
    {
        [Inject]
        protected IStateGroupService? CommonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.StateGroups);
        }

    }
}
