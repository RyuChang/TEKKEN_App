using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveTexts
{
    public partial class BasePageComponent : BaseDataComponent<MoveText, MoveText_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected MoveTextService<MoveText, MoveText_name> commonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.MoveTexts);
        }
    }
}
