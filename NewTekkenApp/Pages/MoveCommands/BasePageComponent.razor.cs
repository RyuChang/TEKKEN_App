using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveCommands
{
    public partial class BasePageComponent : BaseComponent<MoveCommand, MoveCommand_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected MoveCommandService<MoveCommand, MoveCommand_name> CommonService { get; set; } = default!;

        [Inject]
        protected MoveService<Move, Move_name> MoveService { get; set; } = default!;

        [Inject]

        protected StateService<State, State_name> StateService { get; set; } = default!;


        public BasePageComponent()
        {
            SetAppType(AppType.MoveCommands);
        }
    }
}
