using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class MoveService<TDataEntity, TNameEntity> : BaseService<Move, Move_name>
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


    }
}
