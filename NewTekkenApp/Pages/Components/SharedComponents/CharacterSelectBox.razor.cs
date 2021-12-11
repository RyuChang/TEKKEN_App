using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewTekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.SharedComponents
{
    public partial class CharacterSelectBox
    {
        [Inject]
        protected CharacterService<Character, Character_name> characterService { get; set; }

        public List<SelectListItem> selectListItems { get; set; }

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

