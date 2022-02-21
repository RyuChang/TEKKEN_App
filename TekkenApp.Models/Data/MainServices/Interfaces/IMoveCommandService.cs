using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveCommandService : IBaseNameService<MoveCommand, MoveCommand_name>
    {
        Task<MoveCommand> GetDataEntityByBaseCodeAsync(int baseCode);
        Task<List<MoveCommand>> GetEntitiesWithMove();
        Task<List<MoveCommand>> GetEntitiesWithMoveByCharacterCode(int characterCode);
        Task<MoveCommand> GetEntityWithMovesByIdAsync(int id);
        Task<MoveData> UpdateHitTypeAsync(MoveData moveData);
    }
}