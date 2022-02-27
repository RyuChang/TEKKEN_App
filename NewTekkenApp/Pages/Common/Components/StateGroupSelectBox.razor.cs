using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Data;

namespace NewTekkenApp.Pages.Common.Components
{
    public partial class StateGroupSelectBox
    {
        [Inject]
        protected IStateGroupService StateGroupService { get; set; } = default!;

        public List<SelectListItem> selectListItems { get; set; } = default!;

        [Parameter]
        public EventCallback<string> OnClickCallback { get; set; }

        protected override async Task OnInitializedAsync()
        {
            selectListItems = await StateGroupService.GetSelectItems();
        }

        [Parameter]
        public Action<int>? OnStateGroupChanged { get; set; }
    }
}