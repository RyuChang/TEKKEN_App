using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
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

        public async Task<MoveData> GetMoveDataWithMovesByIdAsync(int id)
        {
            return await _dataDbSet.Include(moveData => moveData.Move).ThenInclude(move => move.NameSet).Where(moveData => moveData.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<MoveData>> GetEntitiesWithMoveByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(m => m.Move.Character_code == characterCode).Include(data => data.NameSet).Include(data => data.Move).ToListAsync();
        }

        public async Task<MigrationDataVM> GetMigrationData(string characterName, string description)
        {
            //SqlConnection con= _tekkenDBContext.con


            string connectionString = "Server=(localdb)\\mssqllocaldb; Database = Tekken; Trusted_Connection = True; MultipleActiveResultSets = true";
            SqlConnection con = new SqlConnection(connectionString);
            return con.Query<MigrationDataVM>("[GetFrameData]", new { character_name = characterName, description = description }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            //return await _tekkenDBContext.Database.SqlQuery<>("EXECUTE dbo.GetFrameData @character_name", "KAZUYA").ToListAsync<MigrationDataVM>();
            ////.ToListAsync<MigrationDataVM>();

            //          context.Database.ExecuteSqlCommand("CreateStudents @p0, @p1", parameters: new[] { "Bill", "Gates" });

            // _dataDbSet.Where(m => m.Move.Character_code == characterCode).OrderBy(m => m.Move.Number).Include(d => d.Move).Include(d => d.NameSet).ToListAsync();
        }
    }
}


