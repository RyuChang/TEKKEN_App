using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Components.Base.Data
{
    public partial class ListComponent<TDataEntity, TNameEntity> : BaseComponent<TDataEntity, TNameEntity> where TDataEntity : BaseDataEntity, new()
                                                                                                           where TNameEntity : BaseNameEntity, new()
    {
        [CascadingParameter] public int? StateGroupId { get; set; }
        public IList<TDataEntity> baseEntities { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (StateGroupCode is null)
            {
                baseEntities = await baseService.GetEntitiesWithAllNames();

            }
            else
            {
                baseEntities = await baseService.GetEntitiesWithStateGroup(StateGroupCode.Value);
            }
        }

        public async void GetEntitiesByStateGroup(int stateGroupCode)
        {
            baseEntities = await baseService.GetEntitiesWithStateGroup(stateGroupCode);
            StateHasChanged();
        }

        public async void GetEntitiesByCharacterCode(int characterCode)
        {
            baseEntities = await baseService.GetEntitiesByCharacterCode(characterCode);
            StateHasChanged();
        }
    }
}

