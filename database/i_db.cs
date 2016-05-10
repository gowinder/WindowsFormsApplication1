// gowinder@hotmail.com
// gowinder.database
// i_db.cs
// 2016-05-10-14:11

#region

using MySql.Data.MySqlClient;

#endregion

namespace gowinder.database
{
    public interface i_db
    {
        void open(string host, string db_name, string user_name, string user_pwd, int port);

        MySqlDataReader create_reader(string str_sql);

        int execute_no_query(string str_sql);
    }
}