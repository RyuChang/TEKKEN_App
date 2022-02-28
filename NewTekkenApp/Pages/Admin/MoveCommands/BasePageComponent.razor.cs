using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveCommands
{
    public partial class BasePageComponent : BaseComponent<MoveCommand, MoveCommand_name>
    {
        public int CharacterCode { get; set; }

        [Inject]
        protected IMoveCommandService CommonService { get; set; } = default!;

        [Inject]
        protected IMoveService MoveService { get; set; } = default!;

        [Inject]

        protected IStateService StateService { get; set; } = default!;


        public BasePageComponent()
        {
            SetAppType(AppType.MoveCommands);
        }
    }
}
