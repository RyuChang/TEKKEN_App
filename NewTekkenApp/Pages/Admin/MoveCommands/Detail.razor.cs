using Microsoft.AspNetCore.Components.Web;
using NewTekkenApp.Shared;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveCommands
{
    public partial class Detail : BasePageComponent
    {
        public Move moveEntity { get; set; } = default!;
        private string RawCommand { get; set; } = default!;
        private CommandComponent CommandComponent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            moveEntity = await MoveService.GetMoveListWithCommandsByIdAsync(Id); ;
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (moveEntity is not null)
                {
                    await InitCommand();
                }
            }
        }

        private async Task InitCommand()
        {
            RawCommand = moveEntity.MoveCommand.Command;
            await SetCommand();
        }

        private async Task SetCommand()
        {
            CommandComponent.RawCommand = RawCommand;
            await CommandComponent.SetDisplayCommand();
            StateHasChanged();
            await CommandComponent.StateChanged();

        }

        private async Task SetKeyUp(KeyboardEventArgs e)
        {
            if (e.Key.ToLower() == "n")
            {
                MoveToNextEdit(moveEntity.Number + 1);
            }
        }

    }
}