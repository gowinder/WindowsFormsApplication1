// gowinder@hotmail.com
// gowinder.socket_service_lib
// event_socket_send_package.cs
// 2016-05-04-9:35

#region

using System;
using gowinder.net_base.evnt;

#endregion

namespace gowinder.socket_service_lib.evnt
{
    public class event_socket_send_package : event_send_package
    {
        public override void process()
        {
            var service = to_service as socket_service;
            if (service == null)
                throw new Exception("event_socket_send_package.process to service is not socket_service");

            service.send_package(data as send_package_info);
        }
    }
}