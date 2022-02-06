using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IMoveDataService : IBaseService<MoveData, MoveData_name>
    {
        Task<List<MoveData>> GetEntitiesWithMoveByCharacterCode(int characterCode);
        Task<List<MoveData>> GetEntitiesWithMoves();
        Task<MoveData> GetEntityWithMovesByIdAsync(int id);
        Task<MoveData> UpdateHitTypeAsync(MoveData moveData);
    }
}