using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Components.Base.Data
{
    public partial class CreateComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity, new()
                            where TNameEntity : BaseNameEntity, new()
    {

        protected override async Task OnInitializedAsync()
        {
            baseData = Activator.CreateInstance(typeof(TDataEntity)) as TDataEntity;
            
            if (baseData == null) return;
            baseData.Number = await baseService.GetCreateNumber();
            baseData.Code = await baseService.GetCreateCode(baseData.Number);
            if (StateGroupCode is not null)
            {
                baseData.StateGroup_code = StateGroupCode.Value;
            }
        }

        protected async Task SaveCreate()
        {
            if (baseData == null) return;

            await baseService.CreateEntityAsync(baseData);
            TNameEntity nameEntity = new TNameEntity();
            await baseService.CreateAllNameEntitiesAsync(baseData);
            MoveToDetail(baseData.Id);
        }
    }
}
