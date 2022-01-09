using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.States
{
    public partial class BasePageComponent : BaseDataComponent<State, State_name>
    {
        [Parameter]
        public int? StateGroupCode { get; set; }

        [Inject]
        protected StateService<State, State_name>? CommonService { get; set; }


        public BasePageComponent()
        {
            SetAppType(AppType.States);
        }
    }
}
