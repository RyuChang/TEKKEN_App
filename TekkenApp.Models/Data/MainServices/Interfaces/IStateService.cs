using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IStateService : IBaseService<State, State_name>
    {
        int? StateGroupId { get; set; }
    }
}