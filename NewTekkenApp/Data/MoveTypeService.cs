using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class MoveTypeService<TDataEntity, TNameEntity> : BaseService<MoveType, MoveType_name>

    {

        public MoveTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.moveType, tekkenDbContext.moveType_name)
        {
            mainTable = TableName.MoveType.ToString();
            nameTable = TableName.MoveType_name.ToString();
            //App = AppType.HitType;
        }

        public async Task<MoveType> UpdateHitTypeAsync(MoveType moveType)
        {
            _tekkenDBContext.Entry(moveType).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return moveType;
        }
    }
}


