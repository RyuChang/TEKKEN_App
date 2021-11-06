using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IHitTypeRepository : IBaseDefaultRepository
    {
        //List<HitType> GetAllHitTypes();
        
        /*
        List<HitType> GetHitTypesByCode(int code);
        
        */

        //HitType GetHitType_LastDetail();

        //List<SelectListItem> GetStateGroupsSelectItems(int stateGroup_code);

        //StateGroup GetStateGroup_DetailByStateGroup_code(int stateGroup_code);

        //StateGroup GetStateGroup_LastDetail();

        //void Create(HitType hitType);

        //void Update(StateGroup stateGroup);
        //void Delete(int code);
    }
}
