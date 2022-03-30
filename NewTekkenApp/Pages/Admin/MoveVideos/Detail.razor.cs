using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveVideos
{
    public partial class Detail : BasePageComponent
    {
        public Move moveEntity { get; set; } = default!;
        [Parameter] public string NextNumber { get; set; }



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("NextNumber", out var _nextNumber))
            {
                NextNumber = _nextNumber;
            }

            moveEntity = await MoveService.GetMoveWithMoveVideoByCharacterCodeAndNumberAsync(CharacterCode.Value, int.Parse(NextNumber));
        }
    }
}