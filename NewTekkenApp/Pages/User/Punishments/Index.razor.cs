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
            if (Loading)
            {
                return;
            }
            try
            {
                Loading = true;
                moveLists = await CommonService?.GetMoveListWithCommandsAndVideoByCharacterCodeAsync(CharacterCode);
            }
            finally
            {
                Loading = false;
            }
            StateHasChanged();

        }
    }
}
