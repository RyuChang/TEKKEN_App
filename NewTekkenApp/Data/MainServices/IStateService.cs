using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IStateService : IBaseService<State, State_name>
    {
        int? StateGroupId { get; set; }
    }
}