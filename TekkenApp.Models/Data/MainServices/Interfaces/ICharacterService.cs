using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface ICharacterService : IBaseNameService<Character, Character_name>
    {
        int? CharacterId { get; set; }
    }
}