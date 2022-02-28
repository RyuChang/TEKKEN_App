using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.States
{
    public partial class Index : BasePageComponent
    {
        ListComponent<State, State_name>? childList;

        public void OnStateGroupChanged(int stateGroupCode)
        {
            StateGroupCode = stateGroupCode;
            childList?.GetEntitiesByStateGroup(StateGroupCode);
            StateHasChanged();
        }
    }
}
