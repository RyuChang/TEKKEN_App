using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IStateGroupService : IBaseNameService<StateGroup, StateGroup_name>
    {
        Task<StateGroup> GetStateGroupByIdAsync(int id);
        Task<StateGroup_name> GetStateGroupNameByIdAsync(int id);
        Task<List<StateGroup>> GetStateGroups();
        Task<StateGroup> UpdateStateGroupAsync(StateGroup stateGroup);
    }
}