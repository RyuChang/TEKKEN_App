using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveTypeService : BaseNameService<MoveType, MoveType_name>, IMoveTypeService
    {

        public MoveTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.moveType, tekkenDbContext.moveType_name)
        {
            MainTable = TableName.MoveType.ToString();
            NameTable = TableName.MoveType_name.ToString();
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


