using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using TekkenApp.Models;
using NewTekkenApp.Utilities;

namespace NewTekkenApp.Pages.States
{
    public partial class BasePageComponent : NewTekkenApp.Pages.Components.Base.BaseDataComponent<State, State_name>
    {
        [Inject]
        protected StateService<State, State_name> commonService { get; set; }


        public BasePageComponent()
        {
            SetAppType(AppType.States);
        }

    }
}
