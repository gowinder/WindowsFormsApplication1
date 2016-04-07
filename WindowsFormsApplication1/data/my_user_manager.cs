using gowinder.database.evnt;
using gowinder.game_base_lib.account;
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
        
        public my_user_manager(my_logic_service ser):base(ser)
        {

        }

        public override void rev_async_load_data(event_async_load_db_response response)
        {
            async_load_db_response res = response.data as async_load_db_response;
            
            Dictionary<string, object> load_data = response.data as Dictionary<string, object>;
            foreach (var key in load_data.Keys)
            {
                switch (key)
                {
                    case "list_item":
                        {

                        }
                        break;
                }
            }
        }

        public override void init()
        {
            data_account a = new data_account();
        }
    }
}
