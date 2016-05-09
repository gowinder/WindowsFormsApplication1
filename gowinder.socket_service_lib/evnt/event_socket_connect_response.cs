using gowinder.base_lib;
using gowinder.base_lib.evnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.socket_service_lib.evnt
{
    public class event_socket_connect_response : event_base
    {
        public const string type = "event_socket_connect_response";
        public event_socket_connect_response() : base(type)
        {

        }

        public void set(service_base from, service_base to, socket_connect_info info)
        {
            from_service = from;
            to_service = to;
            data = info;
        }

        public socket_connect_info connect_info
        {
            get
            {
                return data as socket_connect_info;
            }
            set
            {
                data = value;
            }
        }

    }
}
