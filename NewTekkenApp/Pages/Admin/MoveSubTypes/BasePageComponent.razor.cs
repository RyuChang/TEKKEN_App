using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveSubTypes
{
    public partial class BasePageComponent : BaseDataComponent<MoveSubType, MoveSubType_name>
    {
        [Parameter] public int CharacterCode { get; set; }
        [Inject] protected IMoveSubTypeService? CommonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.MoveSubTypes);
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
