using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveService : IBaseNameService<Move, Move_name>
    {
        int? StateGroupId { get; set; }

        Task<Move> GetMoveListWithCommandsByIdAsync(int id);

        Task<IEnumerable<Move>> GetMoveListWithCommandsAndVideoByCharacterCodeAsync(int Character_code);
        Task<IEnumerable<Move>> GetMoveListWithCommandsByCharacterCodeAsync(int Character_code);
        Task<Move> GetMoveListWithCommandsByCharacterCodeAndNumberAsync(int characterCode, int number);
        Task<Move> GetMoveWithMoveDataByCharacterCodeAndNumberAsync(int characterCode, int number);
        Task<Move> GetMoveWithMoveVideoByCharacterCodeAndNumberAsync(int characterCode, int number);
        Task<Move> GetMoveWithMoveDataByIdAsync(int id);
        Task<Move> GetMoveWithMoveVideoByIdAsync(int id);
    }
}