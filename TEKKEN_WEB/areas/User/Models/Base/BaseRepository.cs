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

namespace User.Models
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


    }
}
