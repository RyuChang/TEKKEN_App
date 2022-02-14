using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IMoveSubTypeService : IBaseService<MoveSubType, MoveSubType_name>
    {
        int? StateGroupId { get; set; }
    }
}