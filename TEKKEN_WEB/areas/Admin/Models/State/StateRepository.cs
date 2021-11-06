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
    public class StateRepository : BaseDefaultRepository, IStateRepository
    {
        private IConfiguration _config;
        private SqlConnection con;
        private ILogger<StateRepository> _logger;
        public string language_code = string.Empty;

        public StateRepository(IConfiguration config, ILogger<StateRepository> logger, IHttpContextAccessor httpContextAccessor) : base(config, logger, httpContextAccessor)
        {
            _config = config;
            mainTable = TableName.State;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
        }
        
        public List<State> GetAllStates(int stateGroup_Code = 0) => con.Query<State>("[State_GetAllState]", new { language_code = language_code, stateGroup_Code = stateGroup_Code }, commandType: CommandType.StoredProcedure).ToList();
        
        public List<SelectListItem> GetAllStateSelectItems()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (State state in GetAllStates())
            {
                list.Add(new SelectListItem { Text = state.Name, Value = state.Code.ToString() });
            }
            return list;
        }

        public List<SelectListItem> GetStateByGroupSelectItems(int stateGroup_Code)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (State state in GetAllStates(stateGroup_Code))
            {
                list.Add(new SelectListItem { Text = state.Name, Value = state.Code.ToString() });
            }
            return list;
        }
        /*
        public State GetState_DetailById(int id) => con.Query<State>("[State_GetDetailById]",
       new DynamicParameters(new { language_code = language_code, id = id }),
       commandType: CommandType.StoredProcedure).SingleOrDefault();

        public State GetState_LastDetailByStateGroup_code(int stateGroup_Code) => con.Query<State>("[COMMON_GetLastDetailByGroup]",
                new DynamicParameters(new { tableName = TableName.State.ToString(), stateGroup_Code = stateGroup_Code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();

        public void Create(State state)
        {
            _logger.LogInformation("데이터 입력");
            try
            {
                SaveOrUpdate(state, FormType.Create);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 입력 에러: " + ex);
            }
        }

        public void Update(State state)
        {
            _logger.LogInformation("데이터 수정");
            try
            {
                SaveOrUpdate(state, FormType.Update);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 수정 에러: " + ex);
            }
        }

        public int SaveOrUpdate(State state, FormType formType)
        {
            int result = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@number", value: state.Number, dbType: DbType.Int16);
            parameters.Add("@description", value: state.Description, dbType: DbType.String);

            switch (formType)
            {
                case FormType.Create:
                    //parameters.Add("@code", value: state.Code, dbType: DbType.Int32);
                    parameters.Add("@StateGroup_code", value: state.StateGroup_code, dbType: DbType.Int32);

                    result = con.Execute("[state_CreateState]", parameters
                        , commandType: CommandType.StoredProcedure);
                    break;

                case FormType.Update:
                    parameters.Add("@id", value: state.Id, dbType: DbType.Int16);

                    result = con.Execute("[state_UpdateState]", parameters,
                        commandType: CommandType.StoredProcedure);
                    break;
            }
            return result;
        }

        public void Delete(int code) =>
            con.Execute("[COMMON_DeleteItem]",
                new { tableName = TableName.State.ToString(), code = code },
                commandType: CommandType.StoredProcedure);
        */
    }
}

