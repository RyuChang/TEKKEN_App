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
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public class MoveSubTypeRepository : BaseCharacterRepository, IMoveSubTypeRepository
    {
        private ILogger<MoveSubTypeRepository> _logger;
        
        public MoveSubTypeRepository(IConfiguration config, ILogger<MoveSubTypeRepository> logger, IHttpContextAccessor httpContextAccessor) : base(config, logger, httpContextAccessor)
        {
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            mainTable = TableName.moveSubType;
        }

        private List<MoveSubType> GetAllMoveSubTypes(int character_code) => con.Query<MoveSubType>("[MoveSubType_MoveSubTypesSelectItems]", new { language_code = language_code, character_code = character_code }, commandType: CommandType.StoredProcedure).ToList();

        public List<SelectListItem> GetMoveSubTypesSelectItems(int character_code = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-", Value = "70000001" });
            foreach (MoveSubType moveSubType in GetAllMoveSubTypes(character_code))
            {
                list.Add(new SelectListItem { Text = moveSubType.Name, Value = moveSubType.Code.ToString() });
            }
            return list;
        }


        /*
        public MoveSubType GetMoveSubType_RecentByCharacter_code(int character_code) => con.Query<MoveSubType>("[COMMON_GetlastDetail]",
                new DynamicParameters(new { tableName = TableName.moveSubType.ToString(), character_code = character_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();

        public MoveSubType GetMoveSubType_DetailByCharacter_code(int? character_code = null) => con.Query<MoveSubType>("[MoveSubType_GetDetailByCharacterCode]",
                new DynamicParameters(new { character_code = character_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();
        */
        //public MoveSubType GetMoveSubType_DetailById(int id) => con.Query<MoveSubType>("[MoveSubType_GetDetailById]",
        //        new DynamicParameters(new { id = id, language_code = language_code }),
        //        commandType: CommandType.StoredProcedure).SingleOrDefault();



    }
}
