using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class CharacterService : BaseNameService<Character, Character_name>, ICharacterService
    {
        [CascadingParameter]
        public int? CharacterId { get; set; }

        public CharacterService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.character, tekkenDbContext.character_name)
        {
        }

        public async Task<Character> GetCharacterByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(p => p.Code == characterCode).Include(p => p.NameSet).FirstOrDefaultAsync();
        }
    }
}
