using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveVideos
{
    public partial class BasePageComponent : BaseComponent<MoveVideo, MoveVideo_name>
    {
        [Inject] protected IMoveVideoService CommonService { get; set; } = default!;
        [Inject] protected IMoveService MoveService { get; set; } = default!;

        public BasePageComponent()
        {
            SetAppType(AppType.MoveVideo);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("CharacterCode", out var _characterCode))
            {
                CharacterCode = int.Parse(_characterCode);
            }
            else
            {
                CharacterCode = 0;
            }
        }
    }
}
