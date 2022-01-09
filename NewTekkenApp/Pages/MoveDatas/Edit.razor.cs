using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveDatas
{
    public partial class Edit :
         BasePageComponent
    {
        public MoveData moveDataEntity { get; set; }

        public List<SelectListItem> MoveTypeSelectListItems { get; set; }
        public List<SelectListItem> MoveSubTypeSelectListItems { get; set; }

        public List<SelectListItem> StartTypeSelectListItems { get; set; }
        public List<SelectListItem> HitTypeSelectListItems { get; set; }

        public List<SelectListItem> GuardTypeSelectListItems { get; set; }

        public List<SelectListItem> CounterTypeSelectListItems { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            moveDataEntity = await commonService.GetEntityWithMovesByIdAsync(Id);

            MoveTypeSelectListItems = await moveTypeService.GetSelectItems();

            MoveSubTypeSelectListItems = await moveSubTypeService.GetSelectItems();

            StartTypeSelectListItems = await hitTypeService.GetSelectItems();

            HitTypeSelectListItems = await hitTypeService.GetSelectItems();

            GuardTypeSelectListItems = await hitTypeService.GetSelectItems();

            CounterTypeSelectListItems = await hitTypeService.GetSelectItems();


        }

        protected async Task SaveEdit()
        {


            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }
            await commonService.UpdateDataAsync(moveDataEntity);
            MoveToDetail(Id);
        }

        private void HandleValidSubmit()
        {
            //Logger.LogInformation("HandleValidSubmit called");
        }
    }
}
