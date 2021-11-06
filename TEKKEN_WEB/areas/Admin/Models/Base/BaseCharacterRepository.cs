using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public class BaseCharacterRepository : BaseRepository, IBaseCharacterRepository
    {
        private ILogger<BaseCharacterRepository> _logger;

        public BaseCharacterRepository(IConfiguration config, ILogger<BaseCharacterRepository> logger, IHttpContextAccessor httpContextAccessor) : base(config, logger, httpContextAccessor)
        {
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
            _logger = logger;
        }

        public BaseModel GetRecentBaseModel(int character_code) => con.Query<BaseModel>("[Base_GetNewNumber]",
        new DynamicParameters(new { tableName = mainTable.ToString(), character_code = character_code }),
        commandType: CommandType.StoredProcedure).SingleOrDefault();

        public new BaseModel GetDetailBaseModelById(int id) => con.Query<BaseModel>("[Base_GetDetailById]",
        new DynamicParameters(new { tableName = mainTable.ToString(), id = id, language_code = language_code, BaseType = BaseType.Character_Code.ToString() }),
        commandType: CommandType.StoredProcedure).SingleOrDefault();


        /// <summary>
        /// 데이터 저장, 수정, 답변 공통 메서드
        /// </summary>
        protected override int  SaveOrUpdate(BaseModel baseModel, FormType formType)
        {
            int result = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@tableName", value: mainTable.ToString(), dbType: DbType.String);
            parameters.Add("@number", value: baseModel.Number, dbType: DbType.Int16);
            string name = baseModel.Description.Replace("'", "\'");
            parameters.Add("@description", value: name, dbType: DbType.String);
            parameters.Add("@character_code", value: baseModel.Character_code, dbType: DbType.String);

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

    }
}
