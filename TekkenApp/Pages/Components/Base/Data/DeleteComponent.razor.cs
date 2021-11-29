using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Data
{
    public partial class DeleteComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {

        protected override async Task OnInitializedAsync()
        {
            baseData = await baseService.GetDataEntityByIdAsync(Id);
        }

        protected async Task SaveDelete()
        {
            if (!await JSRuntime.InvokeAsync<bool>("confirm", "삭제 하겠습니까"))
            {
                return;
            }
            await baseService.DeleteDataEntityByIdAsync(baseData);
            MoveToList();
        }
    }
}
