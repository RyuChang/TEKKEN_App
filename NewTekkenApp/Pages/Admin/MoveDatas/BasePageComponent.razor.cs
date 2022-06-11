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

        [Inject] protected ICharacterService CharacterService { get; set; } = default!;
        public BasePageComponent()
        {
            SetAppType(AppType.MoveDatas);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("CharacterCode", out var _characterCode))
            {
                CharacterCode = int.Parse(_characterCode);
            }
            else
            {
                CharacterCode = 0;
            }
        }
    }
}
