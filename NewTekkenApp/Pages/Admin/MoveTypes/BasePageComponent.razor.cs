using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveTypes
{
    [Authorize(Policy = "IsAdmin")]
    public partial class BasePageComponent : BaseDataComponent<MoveType, MoveType_name>
    {
        [Inject]
        protected IMoveTypeService CommonService { get; set; } = default;


        public BasePageComponent()
        {
            SetAppType(AppType.MoveTypes);
        }
    }
}
