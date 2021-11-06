﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IMoveDataRepository : IBaseCharacterRepository
    {
        List<MoveData> GetAllList(BaseType baseType, int character_code);

        MoveData GetMoveDataById(int Id);

        MoveData GetDetailBaseModelById(int id);

        List<MoveData> GetAllTranslateNamesByCode(int code);

        void Merge(MoveData moveData);
    }
}
