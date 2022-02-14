using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveTextService : IBaseService<MoveText, MoveText_name>
    {
        int? StateGroupId { get; set; }
    }
}