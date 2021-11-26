using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using TekkenApp.Data;
using TekkenApp.Models;


namespace TekkenApp.Pages.Components.Base.Data
{
    public partial class EditComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public BaseService<TDataEntity, TNameEntity> baseService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        ILogger<EditComponent<TDataEntity, TNameEntity>> Logger { get; set; }


        public TDataEntity BaseDataEntity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BaseDataEntity = await baseService.GeTDataEntityByIdAsync(Id);
        }

        protected void btnCancel_Click()
        {
            navigationManager.NavigateTo($"{baseService.preUrl}");
        }

        protected void btnList_Click()
        {
            navigationManager.NavigateTo($"{baseService.preUrl}");
        }

        protected async Task btnSave_Click()
        {
            await baseService.UpdateDataAsync(BaseDataEntity);
            navigationManager.NavigateTo($"{baseService.preUrl}/Detail/{BaseDataEntity.Id}");
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
