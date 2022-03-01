using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveCommandService : BaseNameService<MoveCommand, MoveCommand_name>, IMoveCommandService
    {

        public MoveCommandService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.MoveCommand, tekkenDbContext.MoveCommand_name)
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

        public async Task<List<MoveCommand>> GetEntitiesWithMove()
        {
            return await _dataDbSet.Include("Move").Include("NameSet").ToListAsync();
            //return  _dataDbSet.ToList();
        }

        public async Task<MoveCommand> GetEntityWithMovesByIdAsync(int id)
        {
            return await _dataDbSet.Include("Move").Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<MoveCommand>> GetEntitiesWithMoveByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(m => m.Move.Character_code == characterCode).Include(d=>d.Move).Include(d=>d.NameSet).ToListAsync();
        }

        public async Task<MoveCommand> GetDataEntityByBaseCodeAsync(int baseCode)
        {
            return await _dataDbSet.Where(d => d.Base_Code == baseCode).FirstOrDefaultAsync();
        }
    }
}


