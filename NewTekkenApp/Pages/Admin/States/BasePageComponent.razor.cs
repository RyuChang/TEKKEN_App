using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Admin.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.States
{
    public partial class BasePageComponent : BaseDataComponent<State, State_name>
    {
        [Parameter]
        public int? StateGroupCode { get; set; }

        [Inject]
        protected IStateService? CommonService { get; set; }


        public BasePageComponent()
        {
            SetAppType(AppType.States);
        }
    }
}
