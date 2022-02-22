using System.Text.RegularExpressions;
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

        public string TranseCommandToImage(String command)
        {
            var result = $"<img class=\"move\" src=\"/images/[C].svg\" />";
            return Regex.Replace(command, @"\[(\S+?)\]", m => result.Replace("[C]", m.Value.Replace("[", "").Replace("]", "")), RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

    }
}
