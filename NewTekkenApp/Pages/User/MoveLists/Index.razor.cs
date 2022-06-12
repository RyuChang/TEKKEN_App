using System.Globalization;
using System.Runtime.CompilerServices;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.User.MoveLists
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
                moveLists = await CommonService?.GetMoveListWithCommandsByCharacterCodeAsync(CharacterCode);
            }
            finally
            {
                Loading = false;
            }
            StateHasChanged();

        }
    }
}
