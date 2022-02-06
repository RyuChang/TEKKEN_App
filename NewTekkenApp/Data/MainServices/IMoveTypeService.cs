using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IMoveTypeService : IBaseService<MoveType, MoveType_name>
    {
        Task<MoveType> UpdateHitTypeAsync(MoveType moveType);
    }
}