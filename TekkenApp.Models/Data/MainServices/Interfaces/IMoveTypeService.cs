using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveTypeService : IBaseNameService<MoveType, MoveType_name>
    {
        Task<MoveType> UpdateHitTypeAsync(MoveType moveType);
    }
}