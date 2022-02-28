using TekkenApp.Models;

namespace NewTekkenApp.Pages.User.MoveLists
{
    public partial class Index : BasePageComponent
    {
        public IEnumerable<Move> moveLists { get; set; } = default!;

        protected async void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;

            moveLists = await CommonService?.GetMoveListWithCommandsByCharacterCodeAsync(CharacterCode);
            StateHasChanged();

        }
    }
}
