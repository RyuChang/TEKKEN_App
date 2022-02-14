using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveTexts
{
    public partial class BasePageComponent : BaseDataComponent<MoveText, MoveText_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected IMoveTextService? CommonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.MoveTexts);
        }
    }
}
