using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class HitTypeService : BaseService
    {
        [Inject]
        IDbContextFactory<TekkenDbContext> DbFactory { get; set; }

        public HitTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext)
        {
         
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


        public async Task<HitType_name> UpdateHitTypeNameAsync(HitType_name hitType_name)
        {
            //DbContext context = DbFactory.CreateDbContext();

            await UpdateTranslateNameAsync(_tekkenDBContext.hitType_name, hitType_name);
            //HitType_name name = new HitType_name();
            //name.Id = hitType_name.Id;
            //hitType_name.Name = hitType_name.Name;
            //hitType_name.Language_code = hitType_name.Language_code;
            //hitType_name.Base_code = hitType_name.Base_code;

            //_tekkenDBContext.Entry(hitType_name).State = EntityState.Modified;

            //await _tekkenDBContext.SaveChangesAsync();
            return hitType_name;
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
        public async Task<List<HitType_name>> GetHitType_AllTranslateNamesByCodeAsync(int code)
        {
            HitType_name hitType_name = new HitType_name();
            hitType_name.Base_code = code;

            var result = base.GetAllTranslateNamesByCodeAsync(_tekkenDBContext.hitType_name, hitType_name);
            return result;
        }


    }
}


