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
    public class StateGroupRepository : BaseStateGroupRepository, IStateGroupRepository
    {
        private IConfiguration _config;
        private SqlConnection con;
        private ILogger<StateGroupRepository> _logger;
        public string language_code = string.Empty;


        public StateGroupRepository(IConfiguration config, ILogger<StateGroupRepository> logger, IHttpContextAccessor httpContextAccessor) : base(config, logger, httpContextAccessor)
        {
            _config = config;
            mainTable = TableName.StateGroup;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
        }


        public List<StateGroup> GetStateGroups() => con.Query<StateGroup>("[StateGroup_GetStateGroups]", new { language_code = language_code }, commandType: CommandType.StoredProcedure).ToList();

        public List<SelectListItem> GetStateGroupsSelectItems(int stateGroup_code = 1)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (StateGroup stateGroups in GetStateGroups())
            {
                bool selectedItem = false;
                if (stateGroups.Code == stateGroup_code)
                {
                    selectedItem = true;
                }
                list.Add(new SelectListItem { Text = stateGroups.Name, Value = stateGroups.Code.ToString(), Selected = selectedItem });
            }
            return list;
        }
        /*
        public StateGroup GetStateGroup_LastDetail() => con.Query<StateGroup>("[COMMON_GetlastDetail]",
            new DynamicParameters(new { tableName = TableName.StateGroup.ToString()}),
                commandType: CommandType.StoredProcedure).SingleOrDefault();
        */
        /*
        public void Create(StateGroup stateGroup)
        {
            _logger.LogInformation("데이터 입력");
            try
            {
                SaveOrUpdate(stateGroup, FormType.Create);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 입력 에러: " + ex);
            }
        }

        public void Update(StateGroup stateGroup)
        {
            _logger.LogInformation("데이터 수정");
            try
            {
                SaveOrUpdate(stateGroup, FormType.Update);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 수정 에러: " + ex);
            }
        }*/
        /*
        /// <summary>
        /// 데이터 저장, 수정, 답변 공통 메서드
        /// </summary>
        protected override int SaveOrUpdate(BaseModel baseModel, FormType formType)
        {
            int result = 0;
            /*
            var parameters = new DynamicParameters();

            parameters.Add("@number", value: stateGroup.Number, dbType: DbType.Int16);
            parameters.Add("@description", value: stateGroup.Description, dbType: DbType.String);

            switch (formType)
            {
                case FormType.Create:
                    result = con.Execute("[stateGroup_CreateStateGroup]", parameters
                        , commandType: CommandType.StoredProcedure);
                    break;

                case FormType.Update:
                    parameters.Add("@Id", value: stateGroup.Id, dbType: DbType.Int32);

                    result = con.Execute("[stateGroup_UpdateStateGroup]", parameters,
                        commandType: CommandType.StoredProcedure);
                    break;
            }
            return result;
        }

        public StateGroup GetStateGroup_DetailById(int id)
         => con.Query<StateGroup>("[StateGroup_GetDetailById]",
                new DynamicParameters(new { id = id, language_code = language_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();


        
public void Delete(int code) =>
   con.Execute("[COMMON_DeleteItem]",
       new { tableName = TableName.StateGroup.ToString(), code = code },
       commandType: CommandType.StoredProcedure);*/
    }
}
