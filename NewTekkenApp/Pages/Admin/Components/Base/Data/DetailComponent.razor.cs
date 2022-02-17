using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Components.Base.Data
{
    public partial class DetailComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {

        protected override async Task OnInitializedAsync()
        {
            baseData = await baseService.GetDataEntityByIdAsync(Id);
        }
    }
}
