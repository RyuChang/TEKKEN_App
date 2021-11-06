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
    public class MoveTextRepository : BaseCharacterRepository, IMoveTextRepository
    {
        private ILogger<MoveTextRepository> _logger;

        public MoveTextRepository(IConfiguration config, ILogger<MoveTextRepository> logger, IHttpContextAccessor httpContextAccessor) : base(config, logger, httpContextAccessor)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
        }

        public List<MoveText> GetAllMoveTexts(int? character_code = null) => con.Query<MoveText>("[MoveText_GetAllMoveTexts]", new { language_code = language_code, character_code = character_code }, commandType: CommandType.StoredProcedure).ToList();

        public List<SelectListItem> GetAllMoveTextsSelectItems(int? character_code = null)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-", Value = "70000001" });
            foreach (MoveText moveText in GetAllMoveTexts(character_code))
            {
                list.Add(new SelectListItem { Text = moveText.Name, Value = moveText.Code.ToString() });
            }
            return list;
        }
        /*
        public List<SelectListItem> GetMoveTextsByCharacterSelectItems(int character_code)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (MoveText moveText in GetAllMoveTexts(character_code))
            {
                list.Add(new SelectListItem { Text = moveText.Name, Value = moveText.Code.ToString() });
            }
            return list;
        }*/
        /*
        public MoveText GetMoveText_RecentByCharacter_code(int character_code) => con.Query<MoveText>("[COMMON_GetlastDetail]",
                new DynamicParameters(new { tableName = TableName.MoveText.ToString(), character_code = character_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();

        public MoveText GetMoveText_DetailByCharacter_code(int? character_code = null) => con.Query<MoveText>("[MoveText_GetDetailByCharacterCode]",
                new DynamicParameters(new { character_code = character_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();

        public MoveText GetMoveText_DetailById(int id) => con.Query<MoveText>("[MoveText_GetDetailById]",
                new DynamicParameters(new { id = id, language_code = language_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();
        */



    }
}
