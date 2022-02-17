using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
//using NewTekkenApp.Pages.Admin.Components.Base.Data;
//using NewTekkenApp.Pages.Admin.Components.Base.Name;

using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveSubTypes
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
