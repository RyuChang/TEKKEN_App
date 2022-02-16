using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Admin.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Moves
{
    public partial class BasePageComponent : BaseDataComponent<Move, Move_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected IMoveService? CommonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.Moves);
        }
    }
}
