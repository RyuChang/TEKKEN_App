using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.States
{
    public partial class Index : BasePageComponent
    {
        ListComponent<State, State_name>? childList;

        void OnStateGroupChanged(string stateGroupCode)
        {
            if (!string.IsNullOrEmpty(stateGroupCode))
            {
                StateGroupCode = int.Parse(stateGroupCode);
                childList?.GetEntitiesByStateGroup(StateGroupCode.Value);
            }
        }
    }
}
