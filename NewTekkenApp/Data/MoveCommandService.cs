using Microsoft.EntityFrameworkCore;
using NewTekkenApp.Pages.Components.Base.Data;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class MoveCommandService<TDataEntity, TNameEntity> : BaseService<MoveCommand, MoveCommand_name>

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

        //public async Task<MoveCommand> GetEntityWithMovesByIdAsync(int id)
        //{
        //    return await _dataDbSet.Include("Moves").Where(m => m.Id == id).FirstOrDefaultAsync();
        //}

        public async Task<List<MoveCommand>> GetEntitiesWithMoveByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Include("Move").Where(m => m.Move.Character_code == characterCode).ToListAsync();
        }


    }
}


