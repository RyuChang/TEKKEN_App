using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Admin.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveTypes
{
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
