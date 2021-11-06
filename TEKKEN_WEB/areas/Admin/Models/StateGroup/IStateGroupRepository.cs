using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IStateGroupRepository : IBaseRepository
    {
        List<StateGroup> GetStateGroups();

        List<SelectListItem> GetStateGroupsSelectItems(int stateGroup_code);

        //StateGroup GetStateGroup_DetailById(int id);

        //StateGroup GetStateGroup_LastDetail();

        //void Create(StateGroup stateGroup);

        //void Update(StateGroup stateGroup);

        //void Delete(int code);
    }
}
