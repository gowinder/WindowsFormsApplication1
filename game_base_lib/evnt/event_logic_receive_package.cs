// gowinder@hotmail.com
// gowinder.game_base_lib
// event_logic_receive_package.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.net_base.evnt;

#endregion

namespace gowinder.game_base_lib.evnt
{
    public class event_logic_receive_package : event_receive_package
    {
        public override void process()
        {
            var ser = to_service as logic_service;
            if (ser == null)
                throw new Exception("event_http_receive_package.process to service is not logic_serivce");

            var info = data as receive_package_info;
            //    ser.receive_package(this);

            info.package.process_service = to_service;
            info.package.process();
        }
    }
}