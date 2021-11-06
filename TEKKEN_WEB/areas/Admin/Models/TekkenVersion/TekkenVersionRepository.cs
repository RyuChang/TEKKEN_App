using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{

    public class TekkenVersionRepository : ITekkenVersionRepository
    {
        private IConfiguration _config;
        private SqlConnection con;
        private ILogger<TekkenVersionRepository> _logger;


        public TekkenVersionRepository(IConfiguration config, ILogger<TekkenVersionRepository> logger)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
        }

        public TekkenVersion GetVersion()
        {

            String sql = @"SELECT TOP (1) [Version],[Season],[UpdateDate]
            FROM [TEKKEN].[dbo].[TekkenVersion]";

            return con.Query<TekkenVersion>(sql).SingleOrDefault();
        }

        public TekkenVersion GetVersionDetail(float version) => con.Query<TekkenVersion>("[Version_Detail]", new DynamicParameters(new { Version = version }), commandType: CommandType.StoredProcedure).SingleOrDefault();

        public List<TekkenVersion> GetAllVersions() => con.Query<TekkenVersion>("[Version_GetAllVersions]", commandType: CommandType.StoredProcedure).ToList();

        public int UpdateVersion(TekkenVersion model, float version)
        {
            int result = 0;
            _logger.LogInformation("버전 수정");
            try
            {
                result = SaveOrUpdate(model, FormType.Update);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("버전 수정 에러: " + ex);
            }
            return result;
        }

        /// <summary>
        /// 삭제 
        /// </summary>
        public int Remove(float version) => con.Execute("[Version_RemoveVersion]", new { Version = version }, commandType: CommandType.StoredProcedure);

        public int SaveOrUpdate(TekkenVersion model, FormType formType)
        {
            int result = 0;
            var p = new DynamicParameters();

            p.Add("@Version", value: model.Version, dbType: DbType.Decimal);
            p.Add("@Season", value: model.Season, dbType: DbType.Decimal);
            p.Add("@UpdateDate", value: model.UpdateDate, dbType: DbType.Date);

            switch (formType)
            {
                //case BoardWriteFormType.Write:
                //    //[b] 글쓰기 전용
                //    p.Add("@PostIp", value: n.PostIp, dbType: DbType.String);

                //    r = con.Execute("WriteNote", p
                //        , commandType: CommandType.StoredProcedure);
                //    break;
                case FormType.Update:
                    result = con.Execute("[Version_AddVersion]", p,
                        commandType: CommandType.StoredProcedure);
                    break;
            }

            return result;
        }
        public List<SelectListItem> GetAllVersionsSelectItems()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (TekkenVersion tekkenVersion in this.GetAllVersions())
            {
                list.Add(new SelectListItem { Text = tekkenVersion.Version.ToString(), Value = tekkenVersion.Version.ToString() });
            }
            return list;
        }
    }
}


