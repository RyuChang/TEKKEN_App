using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class HitTypeService : BaseNameService<HitType, HitType_name>, IHitTypeService
    {

        public HitTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.HitType, tekkenDbContext.HitType_name)
        {
            MainTable = TableName.HitType.ToString();
            NameTable = TableName.HitType_name.ToString();
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


