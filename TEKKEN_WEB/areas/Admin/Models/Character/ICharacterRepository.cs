using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface ICharacterRepository
    {
        List<Character> GetAllCharacters();
        
        List<SelectListItem> GetAllCharactersSelectItems(int character_code);
        
        Character GetCharacterDetail(int character_code);
        
        int UpdateCharacter(Character model, int character_code);
        
        int SaveOrUpdate(Character model, FormType formType);
        
        int Remove(int id);
    }
}
