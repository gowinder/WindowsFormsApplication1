using gowinder.database.evnt;
using gowinder.game_base_lib.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.service;

namespace WindowsFormsApplication1.data
{
    public class my_account_manager : account_manager
    {

        public my_account_manager(my_logic_service ser) : base(ser)
        {

        }

        protected override void on_rev_async_load_data(data_default_account default_account, event_async_load_db_response response)
        {
            data_account account = default_account as data_account;
            
            Dictionary<string, object> load_data = response.data as Dictionary<string, object>;
            foreach (var key in load_data.Keys)
            {
                switch (key)
                {
                    case "list_item":
                        {
                            account.list_item = load_data[key] as List<data_item>;
                        }
                        break;
                    case "list_fort":
                        {
                            account.list_fort = load_data[key] as List<data_fort>;
                        }
                        break;
                }
            }
        }

        public override void init()
        {
            base.init();
        }

        protected override data_default_account on_create_account()
        {
            return new data_account() as data_default_account;
        }
    }
}
