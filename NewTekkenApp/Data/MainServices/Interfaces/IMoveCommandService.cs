using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IMoveCommandService : IBaseService<MoveCommand, MoveCommand_name>
    {
        Task<List<MoveCommand>> GetEntitiesWithMove();
        Task<List<MoveCommand>> GetEntitiesWithMoveByCharacterCode(int characterCode);
        Task<MoveCommand> GetEntityWithMovesByIdAsync(int id);
        Task<MoveData> UpdateHitTypeAsync(MoveData moveData);
    }
}