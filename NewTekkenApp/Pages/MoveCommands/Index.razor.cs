using NewTekkenApp.Pages.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveCommands
{
    public partial class Index : BasePageComponent
    {
        ListComponent<MoveCommand, MoveCommand_name> childList;
        
        //public IList<TDataEntity> baseEntities { get; set; }
        //protected override async Task OnInitializedAsync()
        //{
        //    base.OnInitializedAsync();

        //    //baseEntities = await baseService.GetEntities();

        //    moveCommandEntities = await commonService.GetEntitiesWithMoves();
        //}

        async void OnCharacterChanged(string characterCode)
        {
            if (!string.IsNullOrEmpty(characterCode))
            {
                CharacterId = int.Parse(characterCode);
                childList.baseEntities= await commonService.GetEntitiesWithMoveByCharacterCode(int.Parse(characterCode));
                StateHasChanged();
            }
            else
            {

            }
        }
    }
}
