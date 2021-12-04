using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.HitTypes
{
    public partial class BasePageComponent : NewTekkenApp.Pages.Components.Base.BaseDataComponent<HitType, HitType_name>
    {
        [Inject]
        protected HitTypeService<HitType, HitType_name> commonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.HitTypes);
        }
    }
}
