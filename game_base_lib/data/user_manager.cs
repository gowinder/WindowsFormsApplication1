using gowinder.database.evnt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.game_base_lib.account
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
