using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CData.SQLSERVER
{
    public class SqlServerService
    {
        public readonly string connectionSqlServerString;
        public SqlServerService(IConfiguration configuration) 
        {
            connectionSqlServerString = configuration.GetConnectionString("SqlServerDBConnection");
        }
        //public List<T> get_list<T>(string query) 
        //{
        //    var conn = new S
        //}
    }
}