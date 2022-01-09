using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveSubTypes
{
    public partial class BasePageComponent : BaseDataComponent<MoveSubType, MoveSubType_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected MoveSubTypeService<MoveSubType, MoveSubType_name>? CommonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.MoveSubTypes);
        }
    }
}
