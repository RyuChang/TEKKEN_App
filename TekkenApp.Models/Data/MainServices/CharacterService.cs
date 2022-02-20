using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class CharacterService : BaseNameService<Character, Character_name>, ICharacterService
    {
        [CascadingParameter]
        public int? CharacterId { get; set; }

        public CharacterService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.character, tekkenDbContext.character_name)
        {
            {

            }
        }



    }
}
