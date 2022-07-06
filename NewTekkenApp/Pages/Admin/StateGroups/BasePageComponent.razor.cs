using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.StateGroups
{
    [Authorize(Policy = "IsAdmin")]
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
