using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveSubTypes
{
    public partial class Index : BasePageComponent
    {
        ListComponent<MoveSubType, MoveSubType_name>? childList;

        void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            childList?.GetEntitiesByCharacterCode(CharacterCode);
        }
    }
}
