using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Moves
{
    public partial class Index : BasePageComponent
    {
        ListComponent<Move, Move_name>? childList;

        void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            childList?.GetEntitiesByCharacterCode(CharacterCode);
            StateHasChanged();
        }
    }
}
