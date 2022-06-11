using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveDataService : IBaseNameService<MoveData, MoveData_name>
    {
        Task<List<MoveData>> GetEntitiesWithMoveByCharacterCode(int characterCode);
        Task<List<MoveData>> GetEntitiesWithMoves();
        Task<MigrationDataVM> GetMigrationData(string characterName, string description);
        Task<MoveData> GetMoveDataWithMovesByIdAsync(int id);
        Task<MoveData> UpdateHitTypeAsync(MoveData moveData);
    }
}