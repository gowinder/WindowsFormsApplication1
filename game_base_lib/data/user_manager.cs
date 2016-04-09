using gowinder.database;
using gowinder.database.evnt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace gowinder.game_base_lib.data
{
    public class user_manager
    {
        public Dictionary<uint, data_default_account> dict_account { get; protected set; }
        public Dictionary<string, data_default_account> dict_account_unique_name { get; protected set; }

        public logic_service service {get; protected set;}
        public user_manager(logic_service ser)
        {
            service = ser;
            dict_account = new Dictionary<uint, data_default_account>();
            dict_account_unique_name = new Dictionary<string, data_default_account>();
        }

        public virtual void rev_async_load_data(event_async_load_db_response response)
        {
            throw new NotImplementedException();
        }

        public virtual void init()
        {
            string str_sql = string.Format("select * from account");
            i_db db = service.get_database(1);
            MySqlDataReader reader = db.create_reader(str_sql);
            while (reader.Read())
            {
                data_default_account account = on_create_account();
                account.read_from_dataset(reader);
                insert_account(account);
            }
        }

        protected void insert_account(data_default_account account)
        {
            dict_account.Add(account.id, account);
            dict_account_unique_name.Add(account.full_name, account);
        }

        protected virtual data_default_account on_create_account()
        {
            return new data_default_account();
        }

        public virtual data_default_account find_account_by_id(uint account_id)
        {
            return dict_account[account_id];
        }

        public virtual data_default_account find_account_by_full_name(string full_name)
        {
            return dict_account_unique_name[full_name];
        }

        public virtual data_default_account find_account_by_platform_and_user_id(uint platform_id, string platform_user_id)
        {
            string full_name = data_default_account.get_full_name(platform_id, platform_user_id);
            return find_account_by_full_name(full_name);
        }
    }
}
