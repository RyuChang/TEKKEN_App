using NewTekkenApp.Pages.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.States
{
    public partial class Index : BasePageComponent
    {
        ListComponent<State, State_name>? childList;

        void OnStateGroupChanged(string stateGroupCode)
        {
            if (!string.IsNullOrEmpty(stateGroupCode))
            {
                childList?.GetEntitiesByStateGroup(int.Parse(stateGroupCode));
                StateHasChanged();
            }
        }
    }
}
