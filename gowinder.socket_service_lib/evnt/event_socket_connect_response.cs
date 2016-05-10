// gowinder@hotmail.com
// gowinder.socket_service_lib
// event_socket_connect_response.cs
// 2016-05-10-14:11

#region

using gowinder.base_lib;
using gowinder.base_lib.evnt;

#endregion

namespace gowinder.socket_service_lib.evnt
{
    public class event_socket_connect_response : event_base
    {
        public const string type = "event_socket_connect_response";

        public event_socket_connect_response() : base(type)
        {
        }

        public socket_connect_info connect_info
        {
            get { return data as socket_connect_info; }
            set { data = value; }
        }

        public void set(service_base from, service_base to, socket_connect_info info)
        {
            from_service = from;
            to_service = to;
            data = info;
            info.band_event = this;
        }
    }
}