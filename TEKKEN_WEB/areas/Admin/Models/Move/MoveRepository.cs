using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TEKKEN_WEB.COMMON.Command;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public class MoveRepository : BaseCharacterRepository, IMoveRepository
    {
        private IConfiguration _config;
        private SqlConnection con;
        private ILogger<MoveRepository> _logger;
        public string language_code = string.Empty;

        public MoveRepository(IConfiguration config, ILogger<MoveRepository> logger, IHttpContextAccessor httpContextAccessor)
            : base(config, logger, httpContextAccessor)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
        }

        public List<Move> GetAllMoves(int character_code = 1) => con.Query<Move>("[Move_GetAllMoves]", new { language_code = language_code, character_code = character_code }, commandType: CommandType.StoredProcedure).ToList();

        public Move GetMoveDetailById(int id) => con.Query<Move>("[Move_GetDetailById]",
                new DynamicParameters(new { Id = id, language_code = language_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();



        //public Move GetMove_RecentByCharacter_code(int character_code) => con.Query<Move>("[Move_RecentDetail]",
        //        new DynamicParameters(new { character_code = character_code }),
        //        commandType: CommandType.StoredProcedure).SingleOrDefault();

        public Move GetMove_RecentByCharacter_code(int character_code) => con.Query<Move>("[COMMON_GetlastDetail]",
                new DynamicParameters(new { tableName = TableName.move.ToString(), character_code = character_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();

        public int Move_GetCodeByNumber(int character_code, int number) => con.Query<int>("[Move_GetCodeByNumber]",
                new DynamicParameters(new { character_code = character_code, number = number }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();
        /// <summary>
        /// 하위 분류 추가
        /// </summary>
        public void Create(Move move)
        {
            _logger.LogInformation("데이터 입력");
            try
            {
                SaveOrUpdate(move, FormType.Create);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 입력 에러: " + ex);
            }
        }

        public void Update(Move move)
        {
            _logger.LogInformation("데이터 수정");
            try
            {
                SaveOrUpdate(move, FormType.Update);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 수정 에러: " + ex);
            }
        }


        /// <summary>
        /// 데이터 저장, 수정, 답변 공통 메서드
        /// </summary>
        public int SaveOrUpdate(Move move, FormType formType)
        {
            int result = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@character_code", value: move.Character_code, dbType: DbType.Int16);
            parameters.Add("@number", value: move.Number, dbType: DbType.Int16);
            parameters.Add("@description", value: move.Description, dbType: DbType.String);
            //parameters.Add("@command", value: move.Command, dbType: DbType.String);
            parameters.Add("@version", value: move.Version, dbType: DbType.Decimal);

            switch (formType)
            {
                case FormType.Create:
                    parameters.Add("@moveCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    result = con.Execute("[Move_CreateMove]", parameters
                        , commandType: CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        move.Code = parameters.Get<int>("@moveCode");
                        CreateForeignCommand(move);
                    }
                    break;



                case FormType.Update:
                    parameters.Add("@id", value: move.Id, dbType: DbType.Int32);
                    result = con.Execute("[Move_UpdateMove]", parameters,
                        commandType: CommandType.StoredProcedure);
                    break;
            }
            return result;
        }

//        public void Delete(int id) => con.Execute("[COMMON_DeleteItem]", new { tableName = TableName.move.ToString(), id = id }, commandType: CommandType.StoredProcedure);

        
        public int CreateForeignCommand(Move move) 
        {
            //new TekkenCommand(move.Command, "en").CreateForeignCommand(move.Code);
            //new TekkenCommand(move.Command, "ko").CreateForeignCommand(move.Code);

            return 0;
        }

    }
}