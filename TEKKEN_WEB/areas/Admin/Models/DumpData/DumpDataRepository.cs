using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public class DumpDataRepository : BaseCharacterRepository,IDumpDataRepository
    {
        private ILogger<MoveDataRepository> _logger;
        private ITranslateNameRepository _translateNameRepository;

        public DumpDataRepository(IConfiguration config, ILogger<MoveDataRepository> logger, IHttpContextAccessor httpContextAccessor, ITranslateNameRepository translateNameRepository) : base(config, logger, httpContextAccessor)
        {
            _logger = logger;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _translateNameRepository = translateNameRepository;
        }

        public List<DumpData> GetAllList(int character_code) => con.Query<DumpData>("[DumpData_GetAllDumps]", new { character_code = character_code }, commandType: CommandType.StoredProcedure).ToList();

        //public MoveData GetMoveDataById(int id) => con.Query<MoveData>("[MoveData_GetMoveDataById]", new { id = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();

        //public List<MoveData> GetAllTranslateNamesByCode(int code) => con.Query<MoveData>("[MoveData_GetTranslateNameByCode]", new
        //{
        //    code = code
        //}, commandType: CommandType.StoredProcedure).ToList();

        //public new MoveData GetDetailBaseModelById(int id) => con.Query<MoveData>("[Base_GetDetailById]",
        //new DynamicParameters(new { tableName = mainTable.ToString(), id = id, language_code = language_code, BaseType = BaseType.Character_Code.ToString() }),
        //commandType: CommandType.StoredProcedure).SingleOrDefault();



        /// <summary>
        /// 데이터 저장, 수정, 답변 공통 메서드
        /// </summary>


        public void Merge(MoveData moveData)
        {
            _logger.LogInformation("데이터 입력");
            try
            {
                SaveOrUpdate(moveData, FormType.Create);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 입력 에러: " + ex);
            }
        }


        protected int SaveOrUpdate(MoveData moveData, FormType formType)
        {
            int result = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@Move_Code", value: moveData.Code, dbType: DbType.Int32);

            parameters.Add("@moveType_code", value: moveData.MoveType_code, dbType: DbType.Int32);
            parameters.Add("@moveSubType_code", value: moveData.MoveSubType_code, dbType: DbType.Int32);


            parameters.Add("@hitCount", value: moveData.HitCount, dbType: DbType.Int16);
            parameters.Add("@hitLevel", value: moveData.HitLevel, dbType: DbType.String);
            parameters.Add("@damage", value: moveData.Damage, dbType: DbType.Int16);

            parameters.Add("@startFrame", value: moveData.StartFrame, dbType: DbType.Int16);
            //parameters.Add("@secondFrame", value: moveData.SecondFrame, dbType: DbType.Int16);
            parameters.Add("@startType_code", value: moveData.StartType_code, dbType: DbType.Int32);

            parameters.Add("@guardFrame", value: moveData.GuardFrame, dbType: DbType.Int16);
            parameters.Add("@guardType_code", value: moveData.GuardType_code, dbType: DbType.Int32);

            parameters.Add("@hitFrame", value: moveData.HitFrame, dbType: DbType.Int16);
            parameters.Add("@hitType_code", value: moveData.HitType_code, dbType: DbType.Int32);

            parameters.Add("@counterFrame", value: moveData.CounterFrame, dbType: DbType.Int16);
            parameters.Add("@counterType_code", value: moveData.CounterType_code, dbType: DbType.Int32);

            parameters.Add("@breakThrow", value: moveData.BreakThrow, dbType: DbType.String);
            parameters.Add("@afterBreak", value: moveData.AfterBreak, dbType: DbType.String);
            //parameters.Add("@version", value: moveData.ver, dbType: DbType.Decimal);

            parameters.Add("@homing", value: moveData.Homing, dbType: DbType.Boolean);
            parameters.Add("@powerCrush", value: moveData.PowerCrush, dbType: DbType.Boolean);
            parameters.Add("@technicallyCrouching", value: moveData.TechnicallyCrouching, dbType: DbType.Boolean);
            parameters.Add("@technicallyJumping", value: moveData.TechnicallyJumping, dbType: DbType.Boolean);
            parameters.Add("@tailSpin", value: moveData.TailSpin, dbType: DbType.Boolean);
            parameters.Add("@wallSplat", value: moveData.WallSplat, dbType: DbType.Boolean);
                     

            result = con.Execute("[moveData_Merge]", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
