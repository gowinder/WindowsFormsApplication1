// gowinder@hotmail.com
// gowinder.http_service_lib
// event_http_send_package.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.net_base.evnt;

#endregion

namespace gowinder.http_service_lib.evnt
{
    [Serializable]
    public class event_http_send_package : event_send_package
    {
        public override void process()
        {
            var service = to_service as http_server_service;
            if (service == null)
                throw new Exception("event_http_send_package.process to service is not http_service");

            service.send_response(data as send_package_info);
        }
    }
}