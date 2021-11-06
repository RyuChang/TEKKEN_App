using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
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
    public class TranslateNameRepository : ITranslateNameRepository
    {
        private IConfiguration _config;
        private SqlConnection con;
        private ILogger<TranslateNameRepository> _logger;
        public string language_code = string.Empty;
        public TableName mainTable { get; set; }
        public TableName nameTable { get; set; }

        public TranslateNameRepository(IConfiguration config, ILogger<TranslateNameRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
        }

        public TranslateName GetTranslateNameByCode(int code, TableName tableName)
            => con.Query<TranslateName>("[ TranslateName_GetTranslateNameByCode]", new DynamicParameters(new { tableName = tableName.ToString(), subTableName = nameTable.ToString(), code = code, language_code = language_code }), commandType: CommandType.StoredProcedure).SingleOrDefault();

        public TranslateName GetTranslateNameById(int id, TableName tableName)
            => con.Query<TranslateName>("[TranslateName_GetTranslateNameById]", new DynamicParameters(new { tableName = tableName.ToString(), subTableName = nameTable.ToString(), id = id}), commandType: CommandType.StoredProcedure).SingleOrDefault();
        
        public BaseModel GetBaseModelById(int id)
            => con.Query<BaseModel>("[TranslateName_GetBaseModelById]", new DynamicParameters(new { tableName = mainTable.ToString(), id = id }), commandType: CommandType.StoredProcedure).SingleOrDefault();

        public List<TranslateName> GetAllTranslateNamesByCode(int code) => con.Query<TranslateName>("[TranslateName_GetTranslateNameByCode]", new { tableName = mainTable.ToString(), subTableName = nameTable.ToString(), code = code}, commandType: CommandType.StoredProcedure).ToList();
               
        public void Create(TranslateName translateName)
        {
            _logger.LogInformation("데이터 수정");
            try
            {
                SaveOrUpdate(translateName, FormType.Create);
            }
            catch (System.Exception ex)
            { 
                _logger.LogError("데이터 수정 에러: " + ex);
            }
        }

        public void Update(TranslateName translateName)
        {
            _logger.LogInformation("데이터 수정");
            try
            {
                SaveOrUpdate(translateName, FormType.Update);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 수정 에러: " + ex);
            }
        }

        public void Merge(TranslateName translateName)
        {
            _logger.LogInformation("데이터 수정");
            try
            {
                SaveOrUpdate(translateName, FormType.Merge);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 수정 에러: " + ex);
            }
        }

        public int SaveOrUpdate(TranslateName translateName, FormType formType)
        {
            int result = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@tableName", value: translateName.TableName.ToString(), dbType: DbType.String);
            parameters.Add("@subTableName", value: nameTable.ToString(), dbType: DbType.String);
            parameters.Add("@name", value: translateName.Name.ToString(), dbType: DbType.String);
            

            switch (formType)
            {
                case FormType.Create:
                    parameters.Add("@language_code", value: translateName.Language_Code, dbType: DbType.String);
                    parameters.Add("@code", value: translateName.Code, dbType: DbType.Int32);

                    result = con.Execute("TranslateName_CreateTranslateName", parameters
                        , commandType: CommandType.StoredProcedure);
                    //throw Exception();
                    
                    break;

                case FormType.Update:
                    parameters.Add("@id", value: translateName.Id, dbType: DbType.Int32);
                    result = con.Execute("[TranslateName_UpdateTranslateName]", parameters,
                        commandType: CommandType.StoredProcedure);
                    break;

                case FormType.Merge:
                    parameters.Add("@language_code", value: translateName.Language_Code, dbType: DbType.String);
                    parameters.Add("@code", value: translateName.Code, dbType: DbType.Int32);
                    
                    result = con.Execute("[TranslateName_Merge]", parameters
                        , commandType: CommandType.StoredProcedure);
                    break;
            }
            return result;
        }

        public void SetTable(TableName tableName, TableName subTableName = TableName.NONE) {
            this.mainTable = tableName;
            this.nameTable = subTableName;
        }

        
    }
}
