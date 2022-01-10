using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewTekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.SharedComponents
{
    public partial class StateGroupSelectBox
    {
        [Inject]
        protected StateGroupService<StateGroup, StateGroup_name> StateGroupService { get; set; } = default!;

        public List<SelectListItem> selectListItems { get; set; } = default!;

        [Parameter]
        public EventCallback<string> OnClickCallback { get; set; }

        protected override async Task OnInitializedAsync()
        {
               selectListItems = await StateGroupService.GetSelectItems();
        }

        [Parameter]
        public Action<String>? OnStateGroupChanged { get; set; }
    }
}