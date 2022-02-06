using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IMoveService : IBaseService<Move, Move_name>
    {
        int? StateGroupId { get; set; }

        Task<Move> GetEntityWithCommandsByIdAsync(int id);
    }
}