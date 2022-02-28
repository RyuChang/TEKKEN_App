using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveDatas
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

        async void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            moveDataEntities = await CommonService.GetEntitiesWithMoveByCharacterCode(CharacterCode);
            StateHasChanged();
        }
    }
}
