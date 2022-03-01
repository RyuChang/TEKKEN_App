using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Components.Base.Data
{
    public partial class ListComponent<TDataEntity, TNameEntity> : BaseComponent<TDataEntity, TNameEntity> where TDataEntity : BaseDataEntity, new()
                                                                                                           where TNameEntity : BaseNameEntity, new()
    {
        public IList<TDataEntity> baseEntities { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (CharacterCode is not null)
            {
                baseEntities = await baseService.GetEntitiesByCharacterCode(CharacterCode.Value);
            }
            else if (StateGroupCode is not null)
            {
                baseEntities = await baseService.GetEntitiesByStateGroup(StateGroupCode.Value);
            }
            else
            {
                baseEntities = await baseService.GetEntitiesWithAllNames();
            }
        }

        public async void GetEntitiesByStateGroup(int stateGroupCode)
        {
            baseEntities = await baseService.GetEntitiesByStateGroup(stateGroupCode);
            StateHasChanged();
        }

        public async void GetEntitiesByCharacterCode(int characterCode)
        {
            baseEntities = await baseService.GetEntitiesByCharacterCode(characterCode);
            StateHasChanged();
        }
    }
}

