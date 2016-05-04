// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// my_account_manager.cs
// 2016-05-04-9:34

#region

using System;
using System.Collections.Generic;
using WindowsFormsApplication1.service;
using gowinder.database.evnt;
using gowinder.game_base_lib.data;

#endregion

namespace WindowsFormsApplication1.data
{
    public class my_account_manager : account_manager
    {
        public my_account_manager(my_logic_service ser) : base(ser)
        {
        }

        protected override void on_rev_async_load_data(data_default_account default_account,
            event_async_load_db_response response)
        {
            var account = default_account as data_account;
            var res = response.data as async_load_db_response;
            if (res == null)
                throw new Exception(
                    "my_account_manager.on_rev_async_load_data response.data is not async_load_db_response");
            var load_data = res.dict_result;
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
            return new data_account();
        }
    }
}