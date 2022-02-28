using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Moves
{
    public partial class BasePageComponent : BaseDataComponent<Move, Move_name>
    {
        public int CharacterCode { get; set; }

        [Inject]
        protected IMoveService? CommonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.Moves);
        }
    }
}
