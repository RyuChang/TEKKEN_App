using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Main
{
    public partial class ListComponent<TEntity, TNameEntity>
                            where TEntity : BaseEntity
                            where TNameEntity : BaseTranslateName, new()
    {
        [Parameter]
        public BaseService<TEntity, TNameEntity> baseService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        public IList<TEntity> baseEntities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            baseEntities = await baseService.GetEntities();
        }

        protected async Task btnDetailName_Click(int Id)
        {
            navigationManager.NavigateTo($"{baseService.preUrl}/Detail_name/{Id}");
        }

        protected async Task btnCreateName_Click(int Code, string Language_code)
        {
            navigationManager.NavigateTo($"{baseService.preUrl}/Create_name/{Code}?Language={Language_code}");
        }
        protected async Task btnEditName_Click(int Id)
        { }
        protected async Task btnDeleteName_Click(int Id)
        { }


        protected async Task btnEditNames_Click()
        {
            //await hitTypeService.UpdateHitTypeNameAsync(hitType_name);
            //navigationManager.NavigateTo($"/HitTypes/Detail_name/{Id}");
        }

        protected async Task btnCreate_Click()
        {

        }

        protected async Task btnEdit_Click(int id)
        {
            navigationManager.NavigateTo($"/HitTypes/Edit/{id}");
        }

        protected async Task btnDetail_Click(int id)
        {
            navigationManager.NavigateTo($"/HitTypes/Detail/{id}");
        }
    }
}
