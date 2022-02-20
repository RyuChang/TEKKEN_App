using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IStateService : IBaseNameService<State, State_name>
    {
        int? StateGroupId { get; set; }
    }
}