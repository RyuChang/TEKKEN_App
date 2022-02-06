using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IMoveSubTypeService : IBaseService<MoveSubType, MoveSubType_name>
    {
        int? StateGroupId { get; set; }
    }
}