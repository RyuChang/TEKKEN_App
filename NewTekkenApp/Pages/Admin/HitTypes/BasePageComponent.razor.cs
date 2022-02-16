using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Admin.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.HitTypes
{
    public partial class BasePageComponent : BaseDataComponent<HitType, HitType_name>
    {
        [Inject]
        protected IHitTypeService? CommonService { get; set; }


        public BasePageComponent()
        {
            SetAppType(AppType.HitTypes);
        }
    }
}
