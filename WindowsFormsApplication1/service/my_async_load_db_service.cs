using gowinder.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.evnt;
using gowinder.base_lib.evnt;
using gowinder.database.evnt;

namespace WindowsFormsApplication1
{
    public class my_async_load_db_service : async_load_db_service
    {
        class my_async_event_builder : i_event_builder
        {
            public event_base build_event(string event_type)
            {
                if(event_type == event_async_load_db_request.type)
                    return new event_my_async_load_db_request();

                return null;
            }
        }

        public my_async_load_db_service():base(async_load_db_service.default_name)
        {
            start_own_thread = true;
        }

        protected override i_event_builder on_create_event_builder()
        {
            return new my_async_event_builder() as i_event_builder; ;
        }
    }
}
