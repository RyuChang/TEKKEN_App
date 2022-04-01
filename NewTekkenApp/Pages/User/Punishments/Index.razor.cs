using System.Globalization;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.User.Punishments
{
    public partial class Index : BasePageComponent
    {
        public IEnumerable<Move> moveLists { get; set; } = default!;

        protected async void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            
           moveLists = await CommonService?.GetMoveListWithCommandsAndVideoByCharacterCodeAsync(CharacterCode);
            StateHasChanged();

        }
    }
}
