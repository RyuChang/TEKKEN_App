using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;
using TekkenApp.Utilities;

namespace TekkenApp.Pages.States
{
    public partial class BasePageComponent : TekkenApp.Pages.Components.Base.BaseDataComponent<State, State_name>
    {
        [Inject]
        protected StateService<State, State_name> commonService { get; set; }
        

        public BasePageComponent()
        {
            SetAppType(AppType.StateGroups);
        }

    }
}
