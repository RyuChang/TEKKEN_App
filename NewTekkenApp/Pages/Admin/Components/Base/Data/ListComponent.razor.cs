using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Admin.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Components.Base.Data
{
    public partial class ListComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {
        public IList<TDataEntity> baseEntities { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            baseEntities = await baseService.GetEntitiesWithName();
        }

        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public async void GetEntitiesByStateGroup(int stateGroupCode)
        {
            baseEntities = await baseService.GetEntitiesWithStateGroup(stateGroupCode);
        }

        public async void GetEntitiesByCharacterCode(int characterCode)
        {
            baseEntities = await baseService.GetEntitiesWithCharacterCode(characterCode);
            StateHasChanged();
        }
    }
}

