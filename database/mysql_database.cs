using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database
{
    class mysql_database : i_database
    {
        MySqlConnection _connection;

        public void open(string host, string db_name, string user_name, string user_pwd, int port)
        {
            string str_conn = string.Format("server={0};User Id={1};password={2};Database={3}", host, db_name, user_pwd, db_name);
            _connection = new MySqlConnection(str_conn);
            _connection.Open();
        }

        public MySqlDataReader create_recordset(string str_sql)
        {
            MySqlCommand cmd = new MySqlCommand(str_sql, _connection);
            return cmd.ExecuteReader();
        }
    }
}
