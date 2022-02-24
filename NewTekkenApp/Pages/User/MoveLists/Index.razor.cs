using TekkenApp.Models;

namespace NewTekkenApp.Pages.User.MoveLists
{
    public partial class Index : BasePageComponent
    {
        public IEnumerable<Move> moveLists { get; set; } = default!;

        protected async void OnCharacterChanged(string characterCode)
        {
            if (!string.IsNullOrEmpty(characterCode))
            {
                CharacterId = int.Parse(characterCode);

                moveLists = await CommonService?.GetMoveListWithCommandsByCharacterCodeAsync(CharacterId.Value);
                StateHasChanged();
            }
            else
            {

            }
        }
    }
}
