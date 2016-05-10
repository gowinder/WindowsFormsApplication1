// gowinder@hotmail.com
// gowinder.database
// mysql_database.cs
// 2016-05-10-14:11

#region

using System;
using MySql.Data.MySqlClient;

#endregion

namespace gowinder.database
{
    internal class mysql_database : i_db
    {
        private MySqlConnection _connection;

        public void open(string host, string db_name, string user_name, string user_pwd, int port)
        {
            var str_conn = string.Format("server={0};User Id={1};password={2};Database={3}", host, user_name, user_pwd,
                db_name);
            _connection = new MySqlConnection(str_conn);
            _connection.Open();
        }

        public MySqlDataReader create_reader(string str_sql)
        {
            var cmd = new MySqlCommand(str_sql, _connection);
            return cmd.ExecuteReader();
        }

        public int execute_no_query(string str_sql)
        {
            throw new NotImplementedException();
            var cmd = new MySqlCommand(str_sql, _connection);

            return cmd.ExecuteNonQuery();
        }
    }
}