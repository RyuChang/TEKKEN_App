using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Data;

namespace NewTekkenApp.Pages.Common.Components
{
    public partial class CharacterSelectBox
    {
        [Inject]
        protected ICharacterService characterService { get; set; } = default!;

        public List<SelectListItem> selectListItems { get; set; } = default!;

        [Parameter]
        public EventCallback<string> OnClickCallback { get; set; }

        protected override async Task OnInitializedAsync()
        {
            selectListItems = await characterService.GetSelectItems();
        }

        [Parameter]
        public Action<String>? OnCharacterChanged { get; set; }
    }
}

