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

        public TDataEntity BaseEntity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BaseEntity = Activator.CreateInstance(typeof(TDataEntity)) as TDataEntity;
            BaseEntity.Number = await BaseService.GetCreateNumber();
            BaseEntity.Code = await BaseService.GetCreateCode(BaseEntity.Number);
        }

        protected async Task btnSave_Click()
        {
            await BaseService.CreateEntityAsync(BaseEntity);
            navigationManager.NavigateTo($"{BaseService.preUrl}/Detail/{BaseEntity.Id}");
        }
    }
}
