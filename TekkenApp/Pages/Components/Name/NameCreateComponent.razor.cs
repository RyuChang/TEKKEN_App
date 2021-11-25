using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Name
{
    public partial class NameCreateComponent<TEntity, TNameEntity>
                                            where TEntity : BaseEntity
                                            where TNameEntity : BaseTranslateName, new()
    {
        [Parameter]
        public BaseService<TEntity, TNameEntity> BaseService { get; set; }

        [Parameter]
        public int Code { get; set; }

        [Parameter]
        public string Language { get; set; }

        public TNameEntity nameEntity = new();

        [Inject]
        NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            nameEntity.Base_code = Code;
            nameEntity.Language_code = Language;
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
        protected async Task btnSave_Click()
        {
            await BaseService.CreateTranslateNameAsync(nameEntity);
            navigationManager.NavigateTo($"{BaseService.preUrl}/Detail_name/{nameEntity.Base_code}");
        }
    }
}
