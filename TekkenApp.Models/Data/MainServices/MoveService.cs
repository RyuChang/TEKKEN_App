using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveService : BaseNameService<Move, Move_name>, IMoveService
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }
        int? IMoveService.StateGroupId { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public MoveService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.Move, tekkenDbContext.Move_name)
        {
            MainTable = TableName.Move.ToString();
            NameTable = TableName.Move_name.ToString();
        }

        public async Task<Move> GetMoveListWithCommandsByIdAsync(int id)
        {
            return await _dataDbSet.Where(m => m.Id == id).Include(m => m.MoveCommand).ThenInclude(c => c.NameSet).FirstOrDefaultAsync();
        }

        public async Task<Move> GetMoveListWithCommandsByCharacterCodeAndNumberAsync(int characterCode, int number)
        {
            return await _dataDbSet.Where(data => data.Character_code == characterCode && data.Number == number).Include(m => m.MoveCommand)
                .ThenInclude(c => c.NameSet)
                .FirstOrDefaultAsync();
        }
        public async Task<Move> GetMoveWithMoveDataByIdAsync(int id)
        {
            return await _dataDbSet.Where(m => m.Id == id).Include(m => m.MoveData).ThenInclude(c => c.NameSet).FirstOrDefaultAsync();
        }
        public async Task<Move> GetMoveWithMoveVideoByIdAsync(int id)
        {
            return await _dataDbSet.Where(m => m.Id == id).Include(m => m.MoveVideo).ThenInclude(c => c.NameSet).FirstOrDefaultAsync();
        }

        public async Task<Move> GetMoveWithMoveDataByCharacterCodeAndNumberAsync(int characterCode, int number)
        {
            return await _dataDbSet.Where(data => data.Character_code == characterCode && data.Number == number).Include(m => m.MoveData)
                .ThenInclude(c => c.NameSet)
                .FirstOrDefaultAsync();
        }

        public async Task<Move> GetMoveWithMoveVideoByCharacterCodeAndNumberAsync(int characterCode, int number)
        {
            return await _dataDbSet.Where(data => data.Character_code == characterCode && data.Number == number).Include(m => m.MoveVideo)
                .ThenInclude(c => c.NameSet)
                .FirstOrDefaultAsync();
        }
        

        public async Task<IEnumerable<Move>> GetMoveListWithCommandsByCharacterCodeAsync(int Character_code)
        {
            return await _dataDbSet.Where(m => m.Character_code == Character_code)
                .Include(m => m.MoveCommand)
                .ThenInclude(c => c.NameSet.Where(n => n.Language_code.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)))
                .Include(m => m.MoveData)
                .ThenInclude(c => c.NameSet)
                .ToListAsync<Move>();
        }
    }
}
