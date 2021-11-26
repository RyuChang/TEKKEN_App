using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Name
{
    public partial class DetailNameComponent<TDataEntity, TNameEntity>
                                            where TDataEntity : BaseDataEntity
                                            where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public BaseService<TDataEntity, TNameEntity> BaseService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public TNameEntity nameEntity { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            nameEntity = await BaseService.GetNameEntityByIdAsync(Id);
        }

        protected void btnCancel_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}");
        }

        protected void btnList_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}");
        }

        protected async Task btnDelete_Click()
        {
            //await hitTypeService.UpdateHitTypeNameAsync(hitType_name);
            //navigationManager.NavigateTo($"/HitTypes/Detail_name/{Id}");
        }
        protected async Task btnEdit_Click(int id)
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}/Edit_name/{Id}");
            //await hitTypeService.UpdateHitTypeNameAsync(hitType_name);
            //navigationManager.NavigateTo($"/HitTypes/Detail_name/{Id}");
        }
    }
}
