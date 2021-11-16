using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class HitTypeService: BaseService<HitType, HitType_name>
    {

        public HitTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.hitType_name)
        {
            mainTable = TableName.HitType.ToString();
            nameTable = TableName.HitType_name.ToString();
        }

        public async Task<HitType> GetHitTypeByIdAsync(int id)
        {
            return await _tekkenDBContext.hitType.FindAsync(id);
        }

        public async Task<HitType_name> GetHitTypeNameByIdAsync(int id)
        {
            return await _tekkenDBContext.hitType_name.FindAsync(id);
        }

        public async Task<List<HitType>> GetHitTypes()
        {
            return await _tekkenDBContext.hitType.ToListAsync();
        }


        public async Task<HitType> UpdateHitTypeAsync(HitType hitType)
        {
            //hitType.Description=
            _tekkenDBContext.Entry(hitType).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return hitType;
        }

        #region Create HitType
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
        public override List<BaseTranslateName> GetEntity_AllTranslateNamesByCodeAsync(int code)
        {
            var result = base.GetAllTranslateNamesByCodeAsync(nameDbSet, code);
            return result;
        }
    }
}


