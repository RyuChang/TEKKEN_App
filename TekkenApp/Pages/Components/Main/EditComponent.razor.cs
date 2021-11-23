using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Main
{
    public partial class EditComponent<TEntity, TNameEntity>
                            where TEntity : BaseEntity
                            where TNameEntity : BaseTranslateName, new()
    {
        [Parameter]
        public BaseService<TEntity, TNameEntity> BaseService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        public TEntity BaseEntity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BaseEntity = await BaseService.GetEntityByIdAsync(Id);
        }

        protected async Task btnSave_Click()
        {
            await BaseService.UpdateDataAsync(BaseEntity);
            navigationManager.NavigateTo($"{BaseService.preUrl}/Detail/{BaseEntity.Id}");
        }

        protected void btnCancel_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}");
        }
        protected void btnList_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}");
        }
        private async Task number_Changed()
        {
            //BaseEntity.Number = await BaseService.GetCreateNumber();
            BaseEntity.Code = await BaseService.GetCreateCode(BaseEntity.Number);
        }

    }
}
