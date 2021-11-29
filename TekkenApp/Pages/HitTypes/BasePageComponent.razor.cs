using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.HitTypes
{
    public partial class BasePageComponent : TekkenApp.Pages.Components.Base.BaseDataComponent<HitType, HitType_name>
    {
        [Inject]
        protected HitTypeService<HitType, HitType_name> commonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.HitTypes);
        }
    }
}
