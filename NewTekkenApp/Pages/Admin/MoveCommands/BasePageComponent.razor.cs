using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveCommands
{
    [Authorize(Policy = "IsAdmin")]
    public partial class BasePageComponent : BaseComponent<MoveCommand, MoveCommand_name>
    {
        //[Parameter] public int CharacterCode { get; set; }
        [Inject] protected IMoveCommandService CommonService { get; set; } = default!;
        [Inject] protected IMoveService MoveService { get; set; } = default!;
        [Inject] protected IStateService StateService { get; set; } = default!;
        [Inject] protected ICommandService CommandService { get; set; } = default!;
        public BasePageComponent()
        {
            SetAppType(AppType.MoveCommands);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("CharacterCode", out var _characterCode))
            {
                CharacterCode = int.Parse(_characterCode);
            }
            else {
                CharacterCode = 0;
            }
        }
    }
}
