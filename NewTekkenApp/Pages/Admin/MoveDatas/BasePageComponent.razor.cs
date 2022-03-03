using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;
namespace NewTekkenApp.Pages.Admin.MoveDatas
{
    public partial class BasePageComponent : BaseComponent<MoveData, MoveData_name>
    {
        [Inject] protected IMoveService MoveService { get; set; } = default!;
        [Inject] protected IMoveDataService CommonService { get; set; } = default!;
        [Inject] protected IMoveTypeService moveTypeService { get; set; } = default!;
        [Inject] protected IMoveSubTypeService moveSubTypeService { get; set; } = default!;
        [Inject] protected IHitTypeService hitTypeService { get; set; } = default!;

        public int CharacterCode { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.MoveDatas);
        }
    }
}
