using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveCommands
{
    public partial class Edit : BasePageComponent
    {
        [CascadingParameter] private IModalService Modal { get; set; }
        [Parameter] public string NextNumber { get; set; }
        private ListComponent<State, State_name>? stateList { get; set; } = default;
        private IList<State>? state;
        private Move moveEntity { get; set; } = default!;

        private int _stateGroupCode { get; set; }
        private string DisplayCommand { get; set; } = default!;
        private string RawCommand { get; set; } = default!;
        private bool keyDown = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("NextNumber", out var _nextNumber))
            {
                NextNumber = _nextNumber;
            }

            if (NextNumber is null)
            {
                moveEntity = await MoveService.GetMoveListWithCommandsByIdAsync(Id); ;
            }
            else
            {
                moveEntity = await MoveService.GetMoveListWithCommandsByCharacterCodeAndNumberAsync(CharacterCode.Value, int.Parse(NextNumber));
            }
            await InitCommand();
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
            MoveToDetail(moveEntity.Id);
        }

        #region 커맨드 입출력 처리
        private async Task InitCommand()
        {
            RawCommand = moveEntity.MoveCommand.Command;
            CommandService.InitCommand(RawCommand);
            await SetCommand();
        }

        private void SetKeyDown(KeyboardEventArgs e)
        {
            CommandService.AddKey(e.Key);
        }

        private async Task SetKeyUp(KeyboardEventArgs e)
        {
            bool isFinish = CommandService.RemoveKey(e.Key);
            if (isFinish)
            {
                await SetCommand();
            }
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

        private async Task TransCommands()
        {
            var nameSet = moveEntity.MoveCommand.NameSet;
            foreach (MoveCommand_name name in nameSet)
            {
                name.Name = await CommandService.TransCommand(RawCommand, name.Language_code);
            }
            await SetCommand();
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
                await SetCommand();
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

