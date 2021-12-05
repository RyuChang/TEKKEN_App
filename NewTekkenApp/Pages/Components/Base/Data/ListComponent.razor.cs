using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.Base.Data
{
    public partial class ListComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {
        public IList<TDataEntity> baseEntities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitializedAsync();
            
            if (StateGroupId == 0 || StateGroupId==null)
            {
                baseEntities = await baseService.GetEntities();
            }
            else {
                baseEntities = await baseService.GetEntitiesWithStateGroup(StateGroupId);
            }
        }

        [CascadingParameter]
        public int? StateGroupId { get; set; }

    }
}

