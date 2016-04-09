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
    class my_user_manager : user_manager
    {

        public my_user_manager(my_logic_service ser) : base(ser)
        {

        }

        public override void rev_async_load_data(event_async_load_db_response response)
        {
            async_load_db_response res = response.data as async_load_db_response;
            data_account account = find_account_by_id(res.account_id) as data_account;
            if (account == null)
                throw new Exception("my_user_manager.rev_async_load_data not found account");
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

            account.full_loaded = true;
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
