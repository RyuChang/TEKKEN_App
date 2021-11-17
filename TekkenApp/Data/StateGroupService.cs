using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class StateGroupService : BaseService<StateGroup, StateGroup_name>
    {
        public StateGroupService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.StateGroup, tekkenDbContext.StateGroup_name)
        {
            mainTable = TableName.StateGroup.ToString();
            nameTable = TableName.StateGroup.ToString();
        }

        public async Task<StateGroup> GetStateGroupByIdAsync(int id)
        {
            return await _tekkenDBContext.StateGroup.FindAsync(id);
        }

        public async Task<StateGroup_name> GetStateGroupNameByIdAsync(int id)
        {
            return await _tekkenDBContext.StateGroup_name.FindAsync(id);
        }

        public async Task<List<StateGroup>> GetStateGroups()
        {
            return await _tekkenDBContext.StateGroup.ToListAsync();
        }


        //public async Task<StateGroup_name> UpdateStateGroupNameAsync(StateGroup_name stateGroup_name)
        //{
        //    await UpdateTranslateNameAsync(_tekkenDBContext.StateGroup_name, stateGroup_name);
        //    return stateGroup_name;
        //}
        //public override async Task<bool> UpdateTranslateNameAsync(BaseTranslateName translateName)
        //{
        //    _tekkenDBContext.Entry(translateName).State = EntityState.Modified;
        //    return true;
        //}


        public async Task<StateGroup> UpdateStateGroupAsync(StateGroup stateGroup)
        {
            _tekkenDBContext.Entry(stateGroup).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return stateGroup;
        }

        #region Create StateGroup
        /*
        public async Task<bool> CreateHitTypeAsync(HitType hitType)
        {
            int newNumber = await GetCreateNumber(TableName.HitType);
            int createCode = await GetCreateCode(TableName.HitType, newNumber, 0, 0);

            hitType.Number = newNumber;
            hitType.Code = createCode;

            await _tekkenDBContext.hitType.AddAsync(hitType);
            await _tekkenDBContext.SaveChangesAsync();


            HitType_name hitType_name = new HitType_name();
            hitType_name.Base_code = createCode;
            hitType_name.Name = hitType.Description;
            
            var result = await base.CreateTranslateNameAsync(hitType_name);


            await _tekkenDBContext.SaveChangesAsync();
            return true;
        }*/
        #endregion
        /*
        public async Task<List<StateGroup_name>> GetStateGroup_AllTranslateNamesByCodeAsync(int code)
        {
            StateGroup_name stateGroup_name = new StateGroup_name();
            stateGroup_name.Base_code = code;

            var result = base.GetAllTranslateNamesByCodeAsync(_tekkenDBContext.StateGroup_name, stateGroup_name);
            return result;
        }*/


        //public override List<StateGroup_name> GetEntity_AllTranslateNamesByCodeAsync(int code)
        //{

        //    var result = base.GetAllTranslateNamesByCodeAsync(code);
        //    return result;
        //}

    }
}


