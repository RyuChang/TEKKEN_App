using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Data;

namespace NewTekkenApp.Pages.Common.Components
{
    public partial class CharacterSelectBox
    {
        [Parameter] public EventCallback<string> OnClickCallback { get; set; }
        [Parameter] public Action<int>? OnCharacterChanged { get; set; }
        [Parameter] public int CharacterCode { get; set; }

        [Inject] protected ICharacterService characterService { get; set; } = default!;

        public List<SelectListItem> selectListItems { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            selectListItems = await characterService.GetSelectItems(true);
        }
    }
}

