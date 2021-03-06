using System;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.Base.Data
{
    public partial class CreateComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity<TNameEntity>
                            where TNameEntity : BaseNameEntity, new()
    {

        protected override async Task OnInitializedAsync()
        {
            baseData = Activator.CreateInstance(typeof(TDataEntity)) as TDataEntity;
            baseData.Number = await baseService.GetCreateNumber();
            baseData.Code = await baseService.GetCreateCode(baseData.Number);
        }

        protected async Task SaveCreate()
        {
            await baseService.CreateEntityAsync(baseData);
            TNameEntity nameEntity = new TNameEntity();
            await baseService.CreateAllNameEntitiesAsync(baseData);
            MoveToDetail(baseData.Id);
        }
    }
}
