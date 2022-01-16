using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveCommands
{
    public partial class Index : BasePageComponent
    {
        public IList<MoveCommand> MoveCommandEntities { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //baseEntities = await baseService.GetEntities();

            MoveCommandEntities = await CommonService.GetEntitiesWithMove();
        }

        async void OnCharacterChanged(string characterCode)
        {
            if (!string.IsNullOrEmpty(characterCode))
            {
                CharacterId = int.Parse(characterCode);
                MoveCommandEntities = await CommonService.GetEntitiesWithMoveByCharacterCode(int.Parse(characterCode));
                StateHasChanged();
            }
        }
    }
}
