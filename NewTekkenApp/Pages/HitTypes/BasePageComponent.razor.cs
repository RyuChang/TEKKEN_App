using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.HitTypes
{
    public partial class BasePageComponent : BaseDataComponent<HitType, HitType_name>
    {
        [Inject]
        protected HitTypeService<HitType, HitType_name0>? CommonService { get; set; }

       

        public BasePageComponent()
        {
            SetAppType(AppType.HitTypes);
        }
    }
}
