using NewTekkenApp.Pages.Common.Components.Base;

using TekkenApp.Models;
namespace NewTekkenApp.Pages.Admin.Components.Base.Name
{
    public partial class DetailNameComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity, new()
                            where TNameEntity : BaseNameEntity, new()
    {

        protected override async Task OnInitializedAsync()
        {
            baseName = await baseService.GetNameEntityByIdAsync(Id);
        }
    }
}
