using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveTexts
{
    public partial class Index : BasePageComponent
    {
        ListComponent<MoveText, MoveText_name>? childList;

        void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            childList?.GetEntitiesByCharacterCode(CharacterCode);
        }
    }
}
