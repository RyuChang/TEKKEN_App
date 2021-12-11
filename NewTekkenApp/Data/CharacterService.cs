using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class CharacterService<TDataEntity, TNameEntity> : BaseService<Character, Character_name>
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
