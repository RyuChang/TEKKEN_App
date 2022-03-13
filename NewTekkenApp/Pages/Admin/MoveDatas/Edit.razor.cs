using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveDatas
{
    public partial class Edit : BasePageComponent
    {
        [Parameter] public string NextNumber { get; set; }
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

            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("NextNumber", out var _nextNumber))
            {
                NextNumber = _nextNumber;
            }
            if (NextNumber is null)
            {
                moveEntity = await MoveService.GetMoveWithMoveDataByIdAsync(Id);
            }
            else
            {
                moveEntity = await MoveService.GetMoveWithMoveDataByCharacterCodeAndNumberAsync(CharacterCode.Value, int.Parse(NextNumber));
            }

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
            await MoveService.UpdateDataAsync(moveEntity);
            MoveToDetail(moveEntity.Id);
        }

        private void HandleValidSubmit()
        {
            //Logger.LogInformation("HandleValidSubmit called");
        }
    }
}
