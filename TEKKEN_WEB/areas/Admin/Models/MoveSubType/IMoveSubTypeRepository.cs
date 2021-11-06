using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IMoveSubTypeRepository : IBaseCharacterRepository
    {
        //List<MoveSubType> GetAllMoveSubTypes(int? character_code = null);

        //List<SelectListItem> GetAllMoveSubTypesSelectItems(int? character_code = null);
        List<SelectListItem> GetMoveSubTypesSelectItems(int character_code = 0);
        //MoveSubType GetMoveSubType_DetailById(int id);
        /*
        MoveSubType GetMoveSubType_DetailByCharacter_code(int? character_code = null);

        MoveSubType GetMoveSubType_RecentByCharacter_code(int character_code);
        */
        //List<SelectListItem> GetMoveSubTypesByCharacterSelectItems(int character_code);

    }
}
