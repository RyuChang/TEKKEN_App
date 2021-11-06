using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IStateRepository : IBaseDefaultRepository
    {
        //State GetStateDetail(string character_code);
        
        List<State> GetAllStates(int stateGroupCode = 0);
        
        List<SelectListItem> GetAllStateSelectItems();

        List<SelectListItem> GetStateByGroupSelectItems(int stateGroup_Code);
        /*
        State GetState_DetailById(int id);

        State GetState_LastDetailByStateGroup_code(int stateGroup_Code);

        void Create(State state);

        void Update(State state);
        
        void Delete(int code);*/
    }
}
