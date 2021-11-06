using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TEKKEN_WEB.Models;

namespace User.Models
{
    public class MoveListRepository : BaseRepository, IMoveListRepository
    {
        private IConfiguration _config;
        private SqlConnection con;
        private ILogger<MoveListRepository> _logger;
        public string language_code = string.Empty;

        public MoveListRepository(IConfiguration config, ILogger<MoveListRepository> logger, IHttpContextAccessor httpContextAccessor)
            : base(config, logger, httpContextAccessor)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
        }

        public List<MoveList> GetAllMoveList(int character_code) => con.Query<MoveList>("[MoveList_GetAllMoveLists]", new { language_code = language_code, character_code = character_code }, commandType: CommandType.StoredProcedure).ToList();

    }
}