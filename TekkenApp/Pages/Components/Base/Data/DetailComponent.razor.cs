using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Data
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
