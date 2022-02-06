using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IMoveTextService : IBaseService<MoveText, MoveText_name>
    {
        int? StateGroupId { get; set; }
    }
}