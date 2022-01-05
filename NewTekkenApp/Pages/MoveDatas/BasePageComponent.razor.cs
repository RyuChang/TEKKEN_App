using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveDatas
{
    public partial class BasePageComponent : BaseComponent<MoveData, MoveData_name>
    {

        [Inject]
        protected MoveDataService<MoveData, MoveData_name> commonService { get; set; }

        [Inject]
        protected MoveTypeService<MoveType, MoveType_name> moveTypeService { get; set; }


        [Inject]
        protected MoveSubTypeService<MoveSubType, MoveSubType_name> moveSubTypeService { get; set; }

        [Inject]
        protected HitTypeService<HitType, HitType_name> hitTypeService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.MoveDatas);
        }
    }
}
