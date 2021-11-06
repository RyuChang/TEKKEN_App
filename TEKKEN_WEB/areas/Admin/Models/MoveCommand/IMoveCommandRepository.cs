using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IMoveCommandRepository : IBaseCharacterRepository
    {
        public List<MoveCommand> GetAllList(BaseType baseType, int character_code);
        public List<MoveCommand> GetMoveCommandById(int Id);

        public void Merge(MoveCommand moveCommand);
        public void UpdateMoveCommandName(MoveCommand[] moveCommand);
    }
}
