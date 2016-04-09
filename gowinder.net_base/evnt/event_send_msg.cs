using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib.evnt;

namespace gowinder.http_service_lib.evnt
{
    public class send_msg_info
    {
        
        public net_context context { get; set; }
        private byte[] buffer { get; set; }
    }

    public class event_send_msg : event_base
    {
        public const string type = "event_send_msg";
    }
}
