using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;
namespace NewTekkenApp.Pages.Admin.MoveDatas
{
    public partial class BasePageComponent : BaseComponent<MoveData, MoveData_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected IMoveDataService CommonService { get; set; } = default!;

        [Inject]
        protected IMoveTypeService moveTypeService { get; set; } = default!;


        [Inject]
        protected IMoveSubTypeService moveSubTypeService { get; set; } = default!;

        [Inject]
        protected IHitTypeService hitTypeService { get; set; } = default!;

        public BasePageComponent()
        {
            SetAppType(AppType.MoveDatas);
        }
    }
}
