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
            baseData.Number = await GetCreateNumber(StateGroupCode, CharacterCode);
            baseData.Code = await baseService.GetCreateCode(baseData.Number, CharacterCode, StateGroupCode);
            
            if (CharacterCode is not null)
            {
                baseData.Character_code = CharacterCode.Value;
            }
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

        private async Task<int> GetCreateNumber(int? stateGroupCode, int? characterCode)
        {
            int result;
            if (StateGroupCode is not null)
            {
                result = await baseService.GetCreateNumberByStateGroup(stateGroupCode.Value);
            }
            else if (CharacterCode is not null)
            {
                result = await baseService.GetCreateNumberByCharacterCode(characterCode.Value);
            }
            else
            {
                result = await baseService.GetCreateNumber();
            }
            return result;
        }
    }
}
