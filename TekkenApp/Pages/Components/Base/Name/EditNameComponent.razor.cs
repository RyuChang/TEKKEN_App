using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Name
{
    public partial class EditNameComponent<TDataEntity, TNameEntity> where TDataEntity : BaseDataEntity
        where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public BaseService<TDataEntity, TNameEntity> BaseService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public TNameEntity BaseNameEntity { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        

        protected override async Task OnInitializedAsync()
        {
            BaseNameEntity = await BaseService.GetNameEntityByIdAsync(Id);
        }
        protected async Task btnSave_Click()
        {
            await BaseService.UpdateTranslateNameAsync(BaseNameEntity);
            navigationManager.NavigateTo($"{BaseService.preUrl}/Detail_name/{BaseNameEntity.Id}");
        }

        protected void btnList_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}");
        }

    }
}
