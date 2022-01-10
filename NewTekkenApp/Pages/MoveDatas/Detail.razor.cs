using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveDatas
{
    public partial class Detail : BasePageComponent
    {
        public MoveData? moveDataEntity { get; set; } = default;

        public List<SelectListItem> MoveTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> MoveSubTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> StartTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> HitTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> GuardTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> CounterTypeSelectListItems { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            moveDataEntity = await CommonService.GetEntityWithMovesByIdAsync(Id);

            MoveTypeSelectListItems = await moveTypeService.GetSelectItems();

            MoveSubTypeSelectListItems = await moveSubTypeService.GetSelectItems();

            StartTypeSelectListItems = await hitTypeService.GetSelectItems();

            HitTypeSelectListItems = await hitTypeService.GetSelectItems();

            GuardTypeSelectListItems = await hitTypeService.GetSelectItems();

            CounterTypeSelectListItems = await hitTypeService.GetSelectItems();
        }
    }
}
