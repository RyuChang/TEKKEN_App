using Admin.Models;
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
    public class HitTypeRepository : BaseDefaultRepository, IHitTypeRepository
    {
        private ILogger<HitTypeRepository> _logger;

        public HitTypeRepository(IConfiguration config, ILogger<HitTypeRepository> logger, IHttpContextAccessor httpContextAccessor) : base(config, logger, httpContextAccessor)
        {
            _logger = logger;
            mainTable = TableName.HitType;
        }
        /*
        public List<SelectListItem> GetHitTypeSelectItems(int stateGroup_code = 1)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (StateGroup stateGroups in GetSelectList())
            {
                bool selectedItem = false;
                if (stateGroups.Code == stateGroup_code)
                {
                    selectedItem = true;
                }
                list.Add(new SelectListItem { Text = stateGroups.Name, Value = stateGroups.Code.ToString(), Selected = selectedItem });
            }
            return list;
        }*/

        /*
        public List<HitType> GetAllHitTypes() => con.Query<HitType>("[HitType_GetAllHitTypes]", new { language_code = language_code }, commandType: CommandType.StoredProcedure).ToList();

        public List<HitType> GetHitTypesByCode(int code) => con.Query<HitType>("[HitType_GetAllHitTypes]", new { code = code }, commandType: CommandType.StoredProcedure).ToList();

        public HitType GetHitType_LastDetail() => con.Query<HitType>("[COMMON_GetlastDetail]",
    new DynamicParameters(new { tableName = TableName.HitType.ToString() }),
        commandType: CommandType.StoredProcedure).SingleOrDefault();

        */
    }
}
