using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveDatas
{
    public partial class BasePageComponent : BaseComponent<MoveData, MoveData_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected MoveDataService<MoveData, MoveData_name> CommonService { get; set; } = default!;

        [Inject]
        protected MoveTypeService<MoveType, MoveType_name> moveTypeService { get; set; } = default!;


        [Inject]
        protected MoveSubTypeService<MoveSubType, MoveSubType_name> moveSubTypeService { get; set; } = default!;

        [Inject]
        protected HitTypeService<HitType, HitType_name> hitTypeService { get; set; } = default!;

        public BasePageComponent()
        {
            SetAppType(AppType.MoveDatas);
        }
    }
}
