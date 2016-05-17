// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// event_my_async_load_db_response.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.database.evnt;
using gowinder.game_logic_lib.service;

#endregion

namespace gowinder.game_logic_lib.evnt
{
    [Serializable]
    public class event_my_async_load_db_response : event_async_load_db_response
    {
        public override void process()
        {
            var ser = to_service as my_logic_service;
            if (ser == null)
                throw new Exception("event_my_async_load_db_response to_service is not my_logic_service");

            ser.account_manager.rev_async_load_data(this);
        }
    }
}