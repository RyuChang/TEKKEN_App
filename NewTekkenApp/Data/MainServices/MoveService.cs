using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class MoveService : BaseService<Move, Move_name>, IMoveService
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public MoveService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.Move, tekkenDbContext.Move_name)
        {
            MainTable = TableName.Move.ToString();
            NameTable = TableName.Move_name.ToString();
        }

        public async Task<Move> GetEntityWithCommandsByIdAsync(int id)
        {
            //
            return await _dataDbSet.Where(m => m.Id == id).Include(m => m.MoveCommand).ThenInclude(c => c.NameSet).FirstOrDefaultAsync();
        }

        //public virtual async Task<List<MoveListVM>> GetMoveStatesByCharacterCode(int character_code)
        //{
        //    return await _dataDbSet.Where(p => p.Character_code == character_code).Select(p => new MoveListVM
        //    {
        //        Code = p.Description,
        //        //Key = p.Code
        //    }).ToListAsync();
        //}

    }
}
