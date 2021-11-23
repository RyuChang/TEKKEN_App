using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Main
{
    public partial class DetailComponent<TEntity, TNameEntity>
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
