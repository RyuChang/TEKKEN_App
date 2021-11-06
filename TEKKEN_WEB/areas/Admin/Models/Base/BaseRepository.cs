using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
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
    abstract public class BaseRepository : IBaseRepository
    {
        protected IConfiguration _config;
        protected SqlConnection con;
        protected ILogger<BaseRepository> _logger;
        public string language_code = string.Empty;
        public TableName mainTable { get; set; }
        public TableName nameTable { get; set; }

        public BaseRepository(IConfiguration config, ILogger<BaseRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
            _logger = logger;
        }

        public BaseModel GetRecentBaseModel() => con.Query<BaseModel>("[Base_GetNewNumber]",
        new DynamicParameters(new { tableName = mainTable.ToString() }),
        commandType: CommandType.StoredProcedure).SingleOrDefault();

        public BaseModel GetDetailBaseModelById(int id) => con.Query<BaseModel>("[Base_GetDetailById]",
        new DynamicParameters(new { tableName = mainTable.ToString(), id = id, language_code = language_code }),
        commandType: CommandType.StoredProcedure).SingleOrDefault();

        public void SetTable(TableName tableName)
        {
            this.mainTable = tableName;
        }
        public void Create(BaseModel baseModel)
        {
            _logger.LogInformation("데이터 입력");
            try
            {
                SaveOrUpdate(baseModel, FormType.Create);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 입력 에러: " + ex);
            }
        }

        public void Update(BaseModel baseModel)
        {
            _logger.LogInformation("데이터 수정");
            try
            {
                SaveOrUpdate(baseModel, FormType.Update);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 수정 에러: " + ex);
            }
        }


        abstract protected int SaveOrUpdate(BaseModel baseModel, FormType formType);
        //}


        /// <summary>
        /// 데이터 저장, 수정, 답변 공통 메서드
        /// </summary>
        /* public int SaveOrUpdate(BaseModel baseModel, FormType formType)
         {
             int result = 0;
             var parameters = new DynamicParameters();

             parameters.Add("@tableName", value: tableName.ToString(), dbType: DbType.String);
             parameters.Add("@number", value: baseModel.Number, dbType: DbType.Int16);
             parameters.Add("@description", value: baseModel.Description, dbType: DbType.String);


             switch (formType)
             {
                 case FormType.Create:
                     result = con.Execute("[Base_Create]", parameters, commandType: CommandType.StoredProcedure);
                     break;

                 case FormType.Update:
                     parameters.Add("@id", value: baseModel.Id, dbType: DbType.Int32);
                     result = con.Execute("[Base_Update]", parameters,
                         commandType: CommandType.StoredProcedure);
                     break;
             }
             return result;
         }
         */

        public void Delete(int id) => con.Execute("[Base_DeleteById]", new { tableName = mainTable.ToString(), id = id }, commandType: CommandType.StoredProcedure);

        public List<BaseModel> GetAllList(BaseType baseType, int? code = null) => con.Query<BaseModel>("[Base_GetAllList]", new { tableName = mainTable.ToString(), code = code, baseType = baseType.ToString() }, commandType: CommandType.StoredProcedure).ToList();



        //public List<BaseModel> GetSelectList() => con.Query<BaseModel>("[Base_GetSelectList]", new { tableName = tableName.ToString(), subTableName = subTableName.ToString(), language_code = language_code }, commandType: CommandType.StoredProcedure).ToList();


        public List<SelectListItem> GetSelectList(int code = 0)
        {
            List<BaseModel> baseModel = con.Query<BaseModel>("[Base_GetSelectList]", new { tableName = mainTable.ToString(), subTableName = nameTable.ToString(), language_code = language_code }, commandType: CommandType.StoredProcedure).ToList();


            List<SelectListItem> list = new List<SelectListItem>();

            foreach (BaseModel item in baseModel)
            {
                bool selectedItem = false;
                if (item.Code == code)
                {
                    selectedItem = true;
                }
                list.Add(new SelectListItem { Text = item.Name, Value = item.Code.ToString(), Selected = selectedItem });
            }
            return list;
        }

        public List<SelectListItem> GetSelectListByCharacter(int character_code = 0)
         {
            List<BaseModel> baseModel = con.Query<BaseModel>("[Base_GetSelectListByCharacter]", new { tableName = mainTable.ToString(), character_code = character_code, language_code = language_code }, commandType: CommandType.StoredProcedure).ToList();


            List<SelectListItem> list = new List<SelectListItem>();

            foreach (BaseModel item in baseModel)
            {
                bool selectedItem = false;
                if (item.Code == character_code)
                {
                    selectedItem = true;
                }
                list.Add(new SelectListItem { Text = item.Name, Value = item.Code.ToString(), Selected = selectedItem });
            }
            return list;
        }

        public List<SelectListItem> GetSelectListByStateGroup(int stateGroup_code, int code = 0)
        {
            List<BaseModel> baseModel = con.Query<BaseModel>("[Base_GetSelectListByStateGroup]", new { tableName = mainTable.ToString(), stateGroup_code = stateGroup_code, language_code = language_code }, commandType: CommandType.StoredProcedure).ToList();


            List<SelectListItem> list = new List<SelectListItem>();

            foreach (BaseModel item in baseModel)
            {
                bool selectedItem = false;
                if (item.Code == code)
                {
                    selectedItem = true;
                }
                list.Add(new SelectListItem { Text = item.Name, Value = item.Code.ToString(), Selected = selectedItem });
            }
            return list;
        }

    }
}
