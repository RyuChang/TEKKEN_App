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
    public class MoveTypeRepository : BaseDefaultRepository, IMoveTypeRepository
    {
        private IConfiguration _config;
        private SqlConnection con;
        private ILogger<MoveTypeRepository> _logger;
        public string language_code = string.Empty;

        public MoveTypeRepository(IConfiguration config, ILogger<MoveTypeRepository> logger, IHttpContextAccessor httpContextAccessor) : base(config, logger, httpContextAccessor)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
            mainTable = TableName.moveType;
            //language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
        }

        public List<MoveType> GetAllMoveTypes() => con.Query<MoveType>("[MoveType_GetAllMoveTypes]", new { language_code = language_code }, commandType: CommandType.StoredProcedure).ToList();

        public List<SelectListItem> GetAllMoveTypesSelectItems()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (MoveType moveType in this.GetAllMoveTypes())
            {
                list.Add(new SelectListItem { Text = moveType.Name, Value = moveType.Code.ToString() });
            }
            return list;
        }
        /*
      public void Create(MoveType model)
      {
          _logger.LogInformation("데이터 입력");
          try
          {
              SaveOrUpdate(model, FormType.Create);
          }
          catch (System.Exception ex)
          {
              _logger.LogError("데이터 입력 에러: " + ex);
          }
      }

      public void Update(MoveType moveType)
      {
          _logger.LogInformation("데이터 수정");
          try
          {
              SaveOrUpdate(moveType, FormType.Update);
          }
          catch (System.Exception ex)
          {
              _logger.LogError("데이터 수정 에러: " + ex);
          }
      }

      /// <summary>
      /// 데이터 저장, 수정, 답변 공통 메서드
      /// </summary>
      public int SaveOrUpdate(MoveType moveType, FormType formType)
      {
          int result = 0;
          var parameters = new DynamicParameters();

          parameters.Add("@number", value: moveType.Number, dbType: DbType.Int16);
          parameters.Add("@description", value: moveType.Description, dbType: DbType.String);


          switch (formType)
          {
              case FormType.Create:
                  result = con.Execute("[MoveType_CreateMoveType]", parameters
                      , commandType: CommandType.StoredProcedure);
                  break;

              case FormType.Update:
                  parameters.Add("@id", value: moveType.Id, dbType: DbType.Int32);
                  parameters.Add("@code", value: moveType.Code, dbType: DbType.Int32);
                  result = con.Execute("[MoveType_UpdateMoveType]", parameters,
                      commandType: CommandType.StoredProcedure);
                  break;
          }
          return result;
      }


      public MoveType GetMoveType_LastDetail() => con.Query<MoveType>("[COMMON_GetlastDetail]",
      new DynamicParameters(new { tableName = TableName.moveType.ToString() }),
      commandType: CommandType.StoredProcedure).SingleOrDefault();

      public MoveType GetMoveType_DetailById(int id) => con.Query<MoveType>("[MoveType_GetDetailById]",
              new DynamicParameters(new { id = id, language_code = language_code }),
              commandType: CommandType.StoredProcedure).SingleOrDefault();

      public void Delete(int code) => con.Execute("[COMMON_DeleteItem]",
          new { tableName = TableName.moveType.ToString(), code = code },
          commandType: CommandType.StoredProcedure);
      */
    }
}

