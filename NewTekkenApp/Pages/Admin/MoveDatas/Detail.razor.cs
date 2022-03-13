using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveDatas
{
    public partial class Detail : BasePageComponent
    {
        public Move moveEntity { get; set; } = default!;

        public List<SelectListItem> MoveTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> MoveSubTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> StartTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> HitTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> GuardTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> CounterTypeSelectListItems { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            moveEntity = await MoveService.GetMoveWithMoveDataByIdAsync(Id);

            MoveTypeSelectListItems = await moveTypeService.GetSelectItems(true);

            MoveSubTypeSelectListItems = await moveSubTypeService.GetSelectItems(true);

            StartTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            HitTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            GuardTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            CounterTypeSelectListItems = await hitTypeService.GetSelectItems(true);
        }
    }
}
