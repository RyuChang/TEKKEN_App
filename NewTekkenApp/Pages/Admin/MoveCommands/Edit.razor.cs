using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveCommands
{
    public partial class Edit : BasePageComponent
    {
        [CascadingParameter] private IModalService Modal { get; set; }
        private ListComponent<State, State_name>? stateList { get; set; } = default;
        private IList<State>? state;
        private Move moveEntity { get; set; } = default!;

        private int _stateGroupCode { get; set; }
        private string DisplayCommand { get; set; } = default!;
        private string rawCommand { get; set; } = default!;
        private bool keyDown = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            moveEntity = await MoveService.GetMoveListWithCommandsByIdAsync(Id); ;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitCommand();
            }
        }

        private async void OnStateGroupChanged(int stateGroupCode)
        {
            if (stateGroupCode > 0)
            {
                _stateGroupCode = stateGroupCode; ;
                state = await StateService?.GetEntitiesWithNameByStateGroup(_stateGroupCode);
                StateHasChanged();
            }
        }

        private async Task SaveEdit()
        {
            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }
            await MoveService.UpdateDataAsync(moveEntity);
            MoveToDetail(Id);
        }

        #region 커맨드 입출력 처리
        private async Task InitCommand()
        {
            rawCommand = moveEntity.MoveCommand.Command;
            CommandService.InitCommand(rawCommand);
            await SetCommand();
        }

        private void SetKeyDown(KeyboardEventArgs e)
        {
            CommandService.AddKey(e.Key);
        }

        private async Task SetKeyUp(KeyboardEventArgs e)
        {
            CommandService.RemoveKey(e.Key);
            await SetCommand();
        }


        private async Task SetCommand()
        {
            await CommandService.SetCommand();
            moveEntity.MoveCommand.Command = CommandService.GetRawCommand();
            //moveEntity.MoveCommand.Description = RawCommand;
            //displayCommand = await CommandService.TransCommand(RawCommand, "");
            DisplayCommand = CommandService.GetDisplayCommand();
            StateHasChanged();
        }



        private async Task AddState(State state)
        {
            string stateGroupType = CommandService.GetStateGroupType(_stateGroupCode);

            if (stateGroupType == "M" || stateGroupType == "T")
            {
                await ShowStateModal(stateGroupType, state.Code);
            }
            else
            {
                CommandService.AddState(stateGroupType, state.Code);
                SetCommand();
            }
        }

        private async Task ShowStateModal(string stateGroupType, int stateCode)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(moveEntity.Character_code), moveEntity.Character_code);

            IModalReference moveData = null; ;
            if (stateGroupType == "M")
            {
                moveData = Modal.Show<MoveListComponent>("State Move", parameters);
            }
            else if (stateGroupType == "T")
            {
                moveData = Modal.Show<MoveTextListComponent>("State Text", parameters);
            }

            var result = await moveData.Result;

            if (result.Data != null)
            {
                CommandService.AddState(stateGroupType, stateCode, (int)result.Data);
                SetCommand();
            }

        }


        #endregion
    }

}

