using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Data
{
    public partial class DetailComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public BaseService<TDataEntity, TNameEntity> BaseService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        public TDataEntity BaseDataEntity { get; set; }


        protected override async Task OnInitializedAsync()
        {
            BaseDataEntity = await BaseService.GeTDataEntityByIdAsync(Id);
        }

        protected async Task btnEdit_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}/Edit/{Id}");
        }

        protected async Task btnDelete_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}/Delete/{Id}");
        }

        protected void btnList_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}");
        }
    }
}
