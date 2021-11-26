using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Data
{
    public partial class CreateComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public BaseService<TDataEntity, TNameEntity> BaseService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        public TDataEntity BaseDataEntity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BaseDataEntity = Activator.CreateInstance(typeof(TDataEntity)) as TDataEntity;
            BaseDataEntity.Number = await BaseService.GetCreateNumber();
            BaseDataEntity.Code = await BaseService.GetCreateCode(BaseDataEntity.Number);
        }

        protected async Task btnSave_Click()
        {
            await BaseService.CreateEntityAsync(BaseDataEntity);
            navigationManager.NavigateTo($"{BaseService.preUrl}/Detail/{BaseDataEntity.Id}");
        }
    }
}
