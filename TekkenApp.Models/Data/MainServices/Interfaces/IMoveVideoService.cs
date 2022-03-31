using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveVideoService : IBaseNameService<MoveVideo, MoveVideo_name>
    {
        Task<List<MoveVideo>> GetEntitiesWithMove();
        Task<List<MoveVideo>> GetEntitiesWithMoveByCharacterCode(int characterCode);
        Task<MoveVideo> GetEntityWithMovesByIdAsync(int id);
        Task UpdateYoutubeVideoInfos(int characterCode);
    }
}