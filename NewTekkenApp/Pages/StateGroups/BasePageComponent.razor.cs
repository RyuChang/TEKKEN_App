using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.StateGroups
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
