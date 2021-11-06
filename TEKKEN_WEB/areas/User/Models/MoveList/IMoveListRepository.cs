using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TEKKEN_WEB.Models;

namespace User.Models
{
    public interface IMoveListRepository
    {
        List<MoveList> GetAllMoveList(int character_code);
    }
}
