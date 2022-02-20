using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveDataService : BaseNameService<MoveData, MoveData_name>, IMoveDataService
    {

        public MoveDataService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.MoveData, tekkenDbContext.MoveData_Name)
        {
            MainTable = TableName.MoveData.ToString();
            NameTable = TableName.MoveData_name.ToString();
            //App = AppType.HitType;
        }

        public async Task<MoveData> UpdateHitTypeAsync(MoveData moveData)
        {
            _tekkenDBContext.Entry(moveData).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return moveData;
        }

        public async Task<List<MoveData>> GetEntitiesWithMoves()
        {
            return await _dataDbSet.Include("Move").Include("NameSet").ToListAsync();
            //return  _dataDbSet.ToList();
        }

        public async Task<MoveData> GetEntityWithMovesByIdAsync(int id)
        {
            return await _dataDbSet.Include("Move").Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<MoveData>> GetEntitiesWithMoveByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Include("Move").Where(m => m.Move.Character_code == characterCode).ToListAsync();
        }

    }
}


