using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.States
{
    public partial class Index : BasePageComponent
    {
        ListComponent<State, State_name>? childList;

        void OnStateGroupChanged(int stateGroupCode)
        {
            if (stateGroupCode > 0)
            {
                childList?.GetEntitiesByStateGroup(stateGroupCode);
            }
        }
    }
}
