﻿using System.Globalization;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.User.Punishments
{
    public partial class Index : BasePageComponent
    {
        public IEnumerable<Move> moveLists { get; set; } = default!;

        protected async void OnCharacterChanged(int characterCode=18)
        {
            CharacterCode = characterCode;
            
           moveLists = await CommonService?.GetMoveListWithCommandsByCharacterCodeAsync(CharacterCode);
            StateHasChanged();

        }
    }
}
