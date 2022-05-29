using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using NewTekkenApp.Pages.Admin.Components.Base.Data;
using NewTekkenApp.Utilities;
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
        private ElementReference commandInput;


        [Inject]
        protected ICookie cookie { get; set; } = default;

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
            if (moveEntity is not null)
            {
                await InitCommand();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await commandInput.FocusAsync();
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
            if (e.Key.ToLower() == "t")
            {
                await TransCommands();
            }

            if (e.Key.ToLower() == "u")
            {
                await SaveEdit();
            }

            if (e.Key.ToLower() == "r")
            {
                await AddRecentState();
            }


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

                await SetRecentState(stateGroupType, state.Code.ToString());

            }

            await SetCommand();

            await commandInput.FocusAsync();
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
                await SetRecentState(stateGroupType, stateCode.ToString(), (int)result.Data);

                SetCommand();
                await commandInput.FocusAsync();
            }

        }

        private async Task SetRecentState(string stateGroupType, string stateCode, int data = 0)
        {
            await cookie.SetValue("stateGroupType", stateGroupType);
            await cookie.SetValue("state", stateCode);
            await cookie.SetValue("data", data.ToString());
        }


        private async Task AddRecentState()
        {
            string stateGroupTypeValue = await cookie.GetValue("stateGroupType");
            int stateValue = int.Parse(await cookie.GetValue("state"));
            int dataValue = int.Parse(await cookie.GetValue("data"));




            if (dataValue != null && dataValue != 0)
            {
                CommandService.AddState(stateGroupTypeValue, stateValue, dataValue);
            }
            else
            {
                CommandService.AddState(stateGroupTypeValue, stateValue);
            }

            SetCommand();
            await commandInput.FocusAsync();
        }
        #endregion
    }

}

