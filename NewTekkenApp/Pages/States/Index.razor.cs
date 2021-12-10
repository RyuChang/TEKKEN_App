//using 

using NewTekkenApp.Pages.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.States
{
    public partial class Index : BasePageComponent
    {
        ListComponent<State, State_name> childList;
        //private string title;
        //public string Title1 { get; set; } = "서비스";


        void OnStateGroupChanged(string stateGroupCode)
        {
            if (string.IsNullOrEmpty(stateGroupCode))
            {
                StateGroupId = int.Parse(stateGroupCode);
                childList.GetEntitiesByStateGroup(int.Parse(stateGroupCode));
                StateHasChanged();
            }
            else
            {
                
            }
        }
    }
}
