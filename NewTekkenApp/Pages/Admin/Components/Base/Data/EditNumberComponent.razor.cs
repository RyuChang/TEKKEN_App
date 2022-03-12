using Microsoft.JSInterop;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Components.Base.Data
{
    public partial class EditNumberComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity, new()
                            where TNameEntity : BaseNameEntity, new()
    {

        protected override async Task OnInitializedAsync()
        {
            baseData = await baseService.GetDataEntityWithAllNameByIdAsync(Id);
        }



        protected async Task SaveEditNumber()
        {

            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }
            int result = await baseService.UpdateNumberAsync(baseData);
            if (result > 0)
            {
                MoveToDetail(baseData.Id);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void HandleValidSubmit()
        {
            //Logger.LogInformation("HandleValidSubmit called");
        }
    }
}
