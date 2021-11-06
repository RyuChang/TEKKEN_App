using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IMoveTypeRepository : IBaseDefaultRepository
    {
        List<MoveType> GetAllMoveTypes();

        List<SelectListItem> GetAllMoveTypesSelectItems();
        /*
        MoveType GetMoveType_LastDetail();

        MoveType GetMoveType_DetailById(int id);

        void Create(MoveType moveType);

        void Update(MoveType moveType);
     
        void Delete(int code);*/
    }
}
