using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IMoveRepository : IBaseCharacterRepository
    {
        List<Move> GetAllMoves(int character_code = 1);

        Move GetMoveDetailById(int id);

        Move GetMove_RecentByCharacter_code(int character_code);

        int Move_GetCodeByNumber(int character_code, int number);
        //List<SelectListItem> GetMovesByCharacterSelectItems(int character_code);
        /*
        void Create(Move move);
        
        void Update(Move move);

        void Delete(int code);*/
    }
}
