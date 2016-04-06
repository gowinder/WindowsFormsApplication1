using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    public interface i_database
    {
        bool open(string host, string db_name, string db_pwd, int port);

        MySqlDataReader create_recordset(string str_sql);
    }
}
