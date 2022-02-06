using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface ICharacterService : IBaseService<Character, Character_name>
    {
        int? CharacterId { get; set; }
    }
}