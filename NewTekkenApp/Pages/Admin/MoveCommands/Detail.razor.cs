using Microsoft.JSInterop;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveCommands
{
    public partial class Detail : BasePageComponent
    {
        private IJSObjectReference? module;

        public Move moveEntity { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            moveEntity = await MoveService.GetDataEntityByIdAsync(Id); ;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./moveCommand.js");
            }
            //if(module is not null)  await module.InvokeAsync<object>("test2");
            if(module is not null)  await module.InvokeAsync<object>("commandUtil.init");
            //await JSRuntime.InvokeAsync<object>("alert");
        }

    }
}