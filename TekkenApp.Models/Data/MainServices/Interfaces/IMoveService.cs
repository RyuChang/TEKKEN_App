using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveService : IBaseService<Move, Move_name>
    {
        int? StateGroupId { get; set; }

        Task<Move> GetEntityWithCommandsByIdAsync(int id);

        Task<Move> GetEntityWithCommandsByCharacterIdAsync(int id);
    }
}