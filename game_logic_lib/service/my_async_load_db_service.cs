// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// my_async_load_db_service.cs
// 2016-05-10-14:11

#region

using gowinder.base_lib.evnt;
using gowinder.database;
using gowinder.database.evnt;
using gowinder.game_logic_lib.evnt;

#endregion

namespace gowinder.game_logic_lib.service
{
    public class my_async_load_db_service : async_load_db_service
    {
        public my_async_load_db_service() : base(default_name)
        {
            start_own_thread = true;
        }

        protected override i_event_builder on_create_event_builder()
        {
            return new my_async_event_builder();
            ;
        }

        private class my_async_event_builder : i_event_builder
        {
            public event_base build_event(string event_type)
            {
                if (event_type == event_async_load_db_request.type)
                    return new event_my_async_load_db_request();

                return null;
            }
        }
    }
}