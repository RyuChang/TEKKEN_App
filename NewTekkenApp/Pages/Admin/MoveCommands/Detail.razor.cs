using Microsoft.JSInterop;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveCommands
{
    public partial class Detail : BasePageComponent
    {
        private IJSObjectReference? module;

        public Move moveEntity { get; set; } = default!;


        private string DisplayCommand { get; set; } = default!;
        private string RawCommand { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                moveEntity = await MoveService.GetMoveListWithCommandsByIdAsync(Id); ;
                await InitCommand();
            }
        }
        private async Task InitCommand()
        {

            RawCommand = moveEntity.MoveCommand.Command;
            CommandService.InitCommand(RawCommand);
            await SetCommand();
        }

        private async Task SetCommand()
        {
            await CommandService.SetCommand();
            moveEntity.MoveCommand.Command = CommandService.GetRawCommand();
            //moveEntity.MoveCommand.Description = RawCommand;
            //displayCommand = await CommandService.TransCommand(RawCommand, "");
            DisplayCommand = CommandService.GetDisplayCommand();
            RawCommand = moveEntity.MoveCommand.Command;
            StateHasChanged();
        }


    }
}