using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Moves
{
    public partial class BasePageComponent : BaseDataComponent<Move, Move_name>
    {
        [Parameter] public int CharacterCode { get; set; }
        [Inject] protected IMoveService? CommonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.Moves);
        }

        protected override Task OnInitializedAsync()
        {
            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("CharacterCode", out var _characterCode))
            {
                CharacterCode = int.Parse(_characterCode);
            }
            return base.OnInitializedAsync();
        }
    }
}
