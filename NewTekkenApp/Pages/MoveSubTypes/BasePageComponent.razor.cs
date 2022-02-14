using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveSubTypes
{
    public partial class BasePageComponent : BaseDataComponent<MoveSubType, MoveSubType_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected IMoveSubTypeService? CommonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.MoveSubTypes);
        }
    }
}
