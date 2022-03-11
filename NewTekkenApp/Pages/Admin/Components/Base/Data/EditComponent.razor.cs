using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Components.Base.Data
{
    public partial class EditComponent<TDataEntity, TNameEntity> :
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
                baseData = await baseService.GetDataEntityByIdAsync(Id);
            }
            else
            {
                baseData = await baseService.GetDataEntityByCharacterCodeAndNumberAsync(CharacterCode.Value, int.Parse(NextNumber));
                Id = baseData.Id;
            }
        }


        protected async Task SaveEdit()
        {

            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }
            await baseService.UpdateDataAsync(baseData);
            MoveToDetail(Id);
        }

        private void HandleValidSubmit()
        {
            //Logger.LogInformation("HandleValidSubmit called");
        }
    }
}
