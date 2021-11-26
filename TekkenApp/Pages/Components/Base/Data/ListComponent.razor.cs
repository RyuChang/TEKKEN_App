using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Data
{
    public partial class ListComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public BaseService<TDataEntity, TNameEntity> baseService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        public IList<TDataEntity> baseEntities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            baseEntities = await baseService.GetEntities();
        }

        protected void btnDetailName_Click(int Id)
        {
            navigationManager.NavigateTo($"{baseService.preUrl}/Detail_name/{Id}");
        }

        protected void btnCreateName_Click(int Code, string Language_code)
        {
            navigationManager.NavigateTo($"{baseService.preUrl}/Create_name/{Code}?Language={Language_code}");
        }
        protected void btnEditName_Click(int Id)
        {
            navigationManager.NavigateTo($"{baseService.preUrl}/Edit_name/{Id}");
        }
        protected void btnDeleteName_Click(int Id)
        { }


        protected void btnEditNames_Click()
        {
            //await hitTypeService.UpdateHitTypeNameAsync(hitType_name);
            //navigationManager.NavigateTo($"/HitTypes/Detail_name/{Id}");
        }

        protected void btnCreate_Click()
        {

        }

        protected void btnEdit_Click(int id)
        {
            navigationManager.NavigateTo($"/HitTypes/Edit/{id}");
        }

        protected void btnDelete_Click(int id)
        {
            navigationManager.NavigateTo($"/HitTypes/Delete/{id}");
        }

        protected void btnDetail_Click(int id)
        {
            navigationManager.NavigateTo($"/HitTypes/Detail/{id}");
        }
    }
}
