using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveVideoService : BaseNameService<MoveVideo, MoveVideo_name>,IMoveVideoService
    {

        public MoveVideoService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.MoveVideo, tekkenDbContext.MoveVideo_name)
        {
            MainTable = TableName.MoveVideo.ToString();
            NameTable = TableName.MoveVideo_name.ToString();
            //App = AppType.HitType;
        }

        public async Task<List<MoveVideo>> GetEntitiesWithMove()
        {
            return await _dataDbSet.Include("Move").Include("NameSet").ToListAsync();
            //return  _dataDbSet.ToList();
        }

        public async Task<MoveVideo> GetEntityWithMovesByIdAsync(int id)
        {
            return await _dataDbSet.Include("Move").Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<MoveVideo>> GetEntitiesWithMoveByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(m => m.Move.Character_code == characterCode).OrderBy(m => m.Move.Number).Include(d => d.Move).Include(d => d.NameSet).ToListAsync();
        }

    }
}


