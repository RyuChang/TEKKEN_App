using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.Base.Name
{
    public partial class DetailNameComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity<TNameEntity>
                            where TNameEntity : BaseNameEntity, new()
    {

        protected override async Task OnInitializedAsync()
        {
            baseName = await baseService.GetNameEntityByIdAsync(Id);
        }
    }
}
