using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveTypes
{
    public partial class BasePageComponent : BaseDataComponent<MoveType, MoveType_name>
    {
        [Inject]
        protected MoveTypeService<MoveType, MoveType_name> CommonService { get; set; } = default;


        public BasePageComponent()
        {
            SetAppType(AppType.MoveTypes);
        }
    }
}
