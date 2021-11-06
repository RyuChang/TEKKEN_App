using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IMoveTextRepository : IBaseCharacterRepository
    {
        //List<MoveText> GetAllMoveTexts(int? character_code = null);
        
        List<SelectListItem> GetAllMoveTextsSelectItems(int? character_code = null);

        //MoveText GetMoveText_DetailById(int id);

        //MoveText GetMoveText_DetailByCharacter_code(int? character_code = null);

        //MoveText GetMoveText_RecentByCharacter_code(int character_code);
        
        //List<SelectListItem> GetMoveTextsByCharacterSelectItems(int character_code);
        
        //void Create(MoveText moveText);
        
        //void Update(MoveText moveText);

        //void Delete(int code);
    }
}
