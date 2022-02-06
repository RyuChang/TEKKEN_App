using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IStateGroupService : IBaseService<StateGroup, StateGroup_name>
    {
        Task<StateGroup> GetStateGroupByIdAsync(int id);
        Task<StateGroup_name> GetStateGroupNameByIdAsync(int id);
        Task<List<StateGroup>> GetStateGroups();
        Task<StateGroup> UpdateStateGroupAsync(StateGroup stateGroup);
    }
}