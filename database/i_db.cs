using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    public interface i_db
    {
        void open(string host, string db_name, string user_name, string user_pwd, int port);

        MySqlDataReader create_reader(string str_sql);

        int execute_no_query(string str_sql);
    }
}
