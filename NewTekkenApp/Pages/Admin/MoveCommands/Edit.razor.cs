using Blazored.Modal.Services;
using Blazored.Modal;
using System.Collections;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using NewTekkenApp.Pages.Admin.Components.Base.Data;
using TekkenApp.Models;
using NewTekkenApp.Utilities;

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
            //    //if(module is not null)  await module.InvokeAsync<object>("test2");
              //  if (module is not null) await module.InvokeAsync<object>("commandUtil.init");
            //    //await JSRuntime.InvokeAsync<object>("alert");

            //    SetKeyMap();
        }

        void OnStateGroupChanged(string stateGroupCode)
        {
            if (!string.IsNullOrEmpty(stateGroupCode))
            {
                _stateGroupCode = int.Parse(stateGroupCode);
                state = StateService?.GetEntitiesWithNameByStateGroup(int.Parse(stateGroupCode));
                StateHasChanged();
            }
        }


        [CascadingParameter] public IModalService Modal { get; set; }

        public async Task ShowMovesModal()
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(moveEntity.Character_code), moveEntity.Character_code);

            var moveModal = Modal.Show<MoveListComponent>("State Move", parameters);
            var result = await moveModal.Result;

            if (result.Cancelled)
            {
                Console.WriteLine("Modal was cancelled");
            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("AddMovesModal", result.Data);

                if (module is not null) await module.InvokeAsync<object>("commandUtil.AddMovesModal", result.Data);
                Console.WriteLine("Modal was closed");
            }
        }

        async Task ShowTextModal()
        {

            //url = "/Admin/MoveText/SelectMoveText";

            //        var result = rawCommand + '/{T:' + stateCode + ':' + $('#move').val() + '}';

        }
        //    void ShowMovesModal() => Modal.Show<MoveListComponent>("State Move");

        private int timer { get; set; } = 0;
        public List<string> resultKey { get; set; }
        public List<string> clickedKey { get; set; }
        private Hashtable keyMap = new Hashtable();
        public string rawCommand = String.Empty;
        public string displayCommand { get; set; }

        Boolean keyDown = false;

        public void InitCommand()
        {
            SetKeyMap();
            rawCommand = moveEntity.MoveCommand.Command;
            
            clickedKey = new List<string>();
            resultKey = new List<string>();

        }
        public void ClearCommand()
        {
            clickedKey.Clear();
            timer = 0;
        }

        public async Task SetKeyDown(KeyboardEventArgs e)
        {
            AddKey(e.Key);
            timer++;
            Console.WriteLine("SetKeyDown");
        }

        public async Task SetKeyUp(KeyboardEventArgs e)
        {
            RemoveKey(e.Key);
            if (resultKey.Count == 0)
            {
                AddCommand(e.Key);
            }
            timer = 0;
            Console.WriteLine("SetKeyUp");

        }

        public void AddKey(string key)
        {

            if (!clickedKey.Contains(key))
            {
                clickedKey.Add(key);
                resultKey.Add(key);
            }
        }

        public void AddCommand(string key)
        {
            string result = rawCommand;
            string formedKey = "";

            if (key == "Backspace")
            {
                int lastIndex = rawCommand.LastIndexOf("/");
                if (lastIndex < 0) lastIndex = 0;
                SetCommand(rawCommand.Substring(0, lastIndex));
                return;
            }
            else if (clickedKey.Count == 1)
            {
                formedKey = key.ToUpper();
            }
            else if (clickedKey.Count >= 2)
            {
                clickedKey.Sort();

                formedKey = String.Join("+", clickedKey.ToArray()).ToUpper();
            }

            if (timer > 2)
            {
                formedKey = 'L' + formedKey.ToUpper();
            }


            string mapppedKey = MapKey(formedKey);


            if (mapppedKey != "")
            {
                result += '/' + mapppedKey;
            }

            SetCommand(result);
            clickedKey.Clear();
        }


        public string MapKey(string formedKey)
        {
            if (keyMap[formedKey] != null)
            {
                return $"[{keyMap[formedKey].ToString().Trim()}]";
            }
            return "";
        }

        public void SetCommand(string result)
        {
            if (result.Length > 0 && result[0] == '/')
            {
                result = result.Substring(1, result.Length - 1);
            }

            rawCommand = result;
            moveEntity.MoveCommand.Command = result;
            moveEntity.MoveCommand.Description = rawCommand;
            //displayCommand = rawCommand.Replace("/", " ");
            displayCommand = CommandLibrary.TranseCommandToImage(rawCommand);
            
            ClearCommand();
        }

        public void RemoveKey(string key)
        {
            bool isExist = this.resultKey.Contains(key);
            if (isExist)
            {
                resultKey.Remove(key);
            }
        }

        public async void SetKeyMap()
        {
            var results = await httpClient.GetFromJsonAsync<MoveListVM[]>("https://localhost:44354/api/commands");

            foreach (var result in results)
            {
                keyMap[result.Key] = result.Code;
            }
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
    }

}

