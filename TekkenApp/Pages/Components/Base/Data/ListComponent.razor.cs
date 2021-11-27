using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Data
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
            baseEntities = await baseService.GetEntities();
        }
    }
}
