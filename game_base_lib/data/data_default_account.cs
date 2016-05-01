using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace gowinder.game_base_lib.data
{
    public class data_default_account
    {
        public data_default_account()
        {
            full_loaded = false;
        }

        public bool full_loaded { get; set; }

        public uint id { get; set; }
        public uint platform_id { get; set; }
        public string platform_user_id { get; set; }
        public string full_name { get; protected set; }
        public DateTime last_operation_date { get; set; }


        public void update_full_name()
        {
            full_name = get_full_name(platform_id, platform_user_id);
        }

        public static string get_full_name(uint platform_id, string platform_user_id)
        {
            return string.Format("_@{0}_@_{1}", platform_id, platform_user_id);
        }

        public void read_from_dataset(MySqlDataReader reader)
        {
            id = (uint)reader[0];
            platform_id = (uint)(int) reader[12];
            platform_user_id = (string) reader[14];
            full_name = (string) reader[1];
        }


        public virtual void on_login()
        {

        }

        public virtual void on_logout()
        {

        }

        public virtual void check_online_timeout()
        {

        }

        public virtual void clear_full_load()
        {

        }

        public virtual void update_operation_time()
        {
            last_operation_date = DateTime.Now;
        }
    }
}
