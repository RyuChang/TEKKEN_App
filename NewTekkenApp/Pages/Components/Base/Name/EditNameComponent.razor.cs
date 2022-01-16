using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NewTekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.Base.Name
{
    public partial class EditNameComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {
        protected override async Task OnInitializedAsync()
        {
            baseName = await baseService.GetNameEntityByIdAsync(Id);
        }
        protected async Task SaveEditName()
        {
            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }

            if (baseName is not null)
            {
                await baseService.UpdateNameEntityAsync(baseName);
                MoveToDetailName(baseName.Id);
            }
        }
    }
}
