using NewTekkenApp.Pages.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveTexts
{
    public partial class Index : BasePageComponent
    {
        ListComponent<MoveText, MoveText_name> childList;

        void OnCharacterChanged(string characterCode)
        {
            if (!string.IsNullOrEmpty(characterCode))
            {
                CharacterId = int.Parse(characterCode);
                childList.GetEntitiesByCharacterCode(int.Parse(characterCode));
                StateHasChanged();
            }
            else
            {

            }
        }
    }
}
