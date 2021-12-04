using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    //public class HitTypeService<TDataEntity, TNameEntity> : BaseService<TDataEntity, TNameEntity>
    public class HitTypeService<TDataEntity, TNameEntity> : BaseService<HitType, HitType_name>

    {

        public HitTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.hitType, tekkenDbContext.hitType_name)
        {
            mainTable = TableName.HitType.ToString();
            nameTable = TableName.HitType_name.ToString();
            //App = AppType.HitType;
        }

        public async Task<HitType> UpdateHitTypeAsync(HitType hitType)
        {
            //hitType.Description=
            _tekkenDBContext.Entry(hitType).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return hitType;
        }
    }
}


