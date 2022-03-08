using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveDatas
{
    public partial class Edit :
         BasePageComponent
    {
        public MoveData moveDataEntity { get; set; } = default!;

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

            MoveTypeSelectListItems = await moveTypeService.GetSelectItems(true);

            MoveSubTypeSelectListItems = await moveSubTypeService.GetSelectItems(true);

            StartTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            HitTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            GuardTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            CounterTypeSelectListItems = await hitTypeService.GetSelectItems(true);
        }

        protected async Task SaveEdit()
        {


            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }
            await CommonService.UpdateDataAsync(moveDataEntity);
            MoveToDetail(Id);
        }

        private void HandleValidSubmit()
        {
            //Logger.LogInformation("HandleValidSubmit called");
        }
    }
}
