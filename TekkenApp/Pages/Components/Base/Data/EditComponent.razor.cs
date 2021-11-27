using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Data
{
    public partial class EditComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {


        protected override async Task OnInitializedAsync()
        {
            BaseDataEntity = await baseService.GetDataEntityByIdAsync(Id);
        }

        protected async Task SaveEdit()
        {

            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }
            await baseService.UpdateDataAsync(BaseDataEntity);
            MoveToDetail(Id);
        }

        private async Task number_Changed(string value)
        {
            int number;
            if (int.TryParse(value, out number))
            {
                BaseDataEntity.Number = number;
                BaseDataEntity.Code = await baseService.GetCreateCode(number);
            }

        }
        private void HandleValidSubmit()
        {
            Logger.LogInformation("HandleValidSubmit called");
        }
    }
}
