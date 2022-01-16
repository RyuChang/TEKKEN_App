using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveDatas
{
    public partial class Index : BasePageComponent
    {
        public IList<MoveData> moveDataEntities { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //baseEntities = await baseService.GetEntities();

            moveDataEntities = await CommonService.GetEntitiesWithMoves();
        }

        async void OnCharacterChanged(string characterCode)
        {
            if (!string.IsNullOrEmpty(characterCode))
            {
                CharacterId = int.Parse(characterCode);
                moveDataEntities = await CommonService.GetEntitiesWithMoveByCharacterCode(int.Parse(characterCode));
                StateHasChanged();
            }
        }
    }
}
