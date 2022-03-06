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
        [Inject] HttpClient httpClient { get; set; }
        ListComponent<State, State_name>? stateList { get; set; } = default;
        IList<State>? state;
        private IJSObjectReference? module;

        public Move moveEntity { get; set; } = default!;
        public int _stateGroupCode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            moveEntity = await MoveService.GetMoveListWithCommandsByIdAsync(Id); ;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                InitCommand();
            }
        }

        protected async void OnStateGroupChanged(int stateGroupCode)
        {
            if (stateGroupCode > 0)
            {
                _stateGroupCode = stateGroupCode; ;
                state = await StateService?.GetEntitiesWithNameByStateGroup(_stateGroupCode);
                StateHasChanged();
            }
        }

        //private int timer { get; set; } = 0;

        public string DisplayCommand { get; set; }
        public string rawCommand { get; set; }

        Boolean keyDown = false;

        public void InitCommand()
        {
            rawCommand = moveEntity.MoveCommand.Command;
            CommandService.InitCommand(rawCommand);
        }

        public async Task SetKeyDown(KeyboardEventArgs e)
        {
            //timer++;
            CommandService.AddKey(e.Key);
            Console.WriteLine("SetKeyDown");
        }

        public async Task SetKeyUp(KeyboardEventArgs e)
        {
            CommandService.RemoveKey(e.Key);
            //timer = 0;
            Console.WriteLine("SetKeyUp");
            SetCommand();
        }


        public async void SetCommand()
        {
            // CommandService.SetCommand(result);

            moveEntity.MoveCommand.Command = CommandService.GetRawCommand();
            //moveEntity.MoveCommand.Description = RawCommand;
            //displayCommand = await CommandService.TransCommand(RawCommand, "");
            DisplayCommand = CommandService.GetDisplayCommand();
            StateHasChanged();
        }


        protected async Task SaveEdit()
        {

            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }
            await MoveService.UpdateDataAsync(moveEntity);
            MoveToDetail(Id);
        }

        #region State 처리
        [CascadingParameter] public IModalService Modal { get; set; }

        public async Task AddState(State state)
        {
            string stateGroupType = CommandService.GetStateGroupType(_stateGroupCode);

            if (stateGroupType == "M" || stateGroupType == "T")
            {
                await ShowStateModal(stateGroupType, state.Code);
            }
            else
            {
                await CommandService.AddState(stateGroupType, state.Code);
                SetCommand();
            }
        }

        public async Task ShowStateModal(string stateGroupType, int stateCode)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(moveEntity.Character_code), moveEntity.Character_code);

            IModalReference moveData = null;;
            if (stateGroupType == "C")
            {
                moveData = Modal.Show<MoveListComponent>("State Move", parameters);
            }
            else if (stateGroupType == "M")
            {
                moveData = Modal.Show<MoveTextListComponent>("State MoveText", parameters);
            }

            var result = await moveData.Result;

            if (result.Data != null)
            {
                await CommandService.AddState(stateGroupType, stateCode,(int)result.Data);
                SetCommand();
            }

        }


        #endregion
    }

}

