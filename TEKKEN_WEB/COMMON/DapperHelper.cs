using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace TEKKEN_WEB.COMMON.Dapper
{
    public class DapperHelper
    {
        public static IConfiguration _config;
        public static SqlConnection _con;
        public static string connectionString;

        static DapperHelper()
        {
            //connectionString = _config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value;
            connectionString = "Server=RC-PC\\GMGG; Database=TEKKEN;Trusted_Connection=false;MultipleActiveResultSets=true;USER ID=sa;Password=dufma12#";
            _con = new SqlConnection(connectionString);
            //_con = 
        }

        public static SqlConnection Con()
        {
            return _con = new SqlConnection(connectionString);
        }
            public static string test() {
            string result = connectionString;
            return result;
        }

    }

}