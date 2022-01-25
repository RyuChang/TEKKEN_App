using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NewTekkenApp.Pages.Components.Base.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveCommands
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
            moveEntity = await MoveService.GetEntityWithCommandsByIdAsync(Id); ;
            

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./moveCommand.js");
            }
            InitCommand();
            //    //if(module is not null)  await module.InvokeAsync<object>("test2");
            //    if (module is not null) await module.InvokeAsync<object>("commandUtil.init");
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
    }
}

