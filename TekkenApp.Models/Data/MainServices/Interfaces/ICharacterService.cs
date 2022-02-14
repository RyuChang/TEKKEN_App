using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface ICharacterService : IBaseService<Character, Character_name>
    {
        int? CharacterId { get; set; }
    }
}