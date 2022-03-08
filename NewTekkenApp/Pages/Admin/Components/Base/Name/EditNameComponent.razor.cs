using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Components.Base.Name
{
    public partial class EditNameComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity, new()
                            where TNameEntity : BaseNameEntity, new()
    {
        [Parameter] public string NextNumber { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("NextNumber", out var _nextNumber))
            {
                NextNumber = _nextNumber;
            }


            if (NextNumber is null)
            {
                baseName = await baseService.GetNameEntityByIdAsync(Id);
            }
            else
            {
                baseName = await baseService.GetNameEntityByCharacterCodeAndNumberAsync(CharacterCode.Value, int.Parse(NextNumber));
            }
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
