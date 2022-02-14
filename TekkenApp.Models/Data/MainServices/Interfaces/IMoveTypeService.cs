using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveTypeService : IBaseService<MoveType, MoveType_name>
    {
        Task<MoveType> UpdateHitTypeAsync(MoveType moveType);
    }
}