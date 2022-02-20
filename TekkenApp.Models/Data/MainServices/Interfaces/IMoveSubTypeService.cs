using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveSubTypeService : IBaseNameService<MoveSubType, MoveSubType_name>
    {
        int? StateGroupId { get; set; }
    }
}