﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveService : IBaseNameService<Move, Move_name>
    {
        int? StateGroupId { get; set; }

        Task<Move> GetMoveListWithCommandsByIdAsync(int id);

        Task<IEnumerable<Move>> GetMoveListWithCommandsByCharacterCodeAsync(int Character_code);
    }
}