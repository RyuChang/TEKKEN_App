using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveTextService : IBaseNameService<MoveText, MoveText_name>
    {
        int? StateGroupId { get; set; }
    }
}