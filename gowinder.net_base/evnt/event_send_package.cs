using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib.evnt;
using gowinder.base_lib;
using gowinder.net_base;

namespace gowinder.http_service_lib.evnt
{
    [Serializable]
    public class send_package_info
    {
        
        public net_context context { get; set; }
        private byte[] buffer { get; set; }
    }

    [Serializable]
    public class event_send_package : event_base
    {
        public const string type = "event_send_package";

        public event_send_package() : base(type)
        {
            
        }

        public void set(service_base from_ser, service_base to_ser, net_package package)
        {
            from_service = from_ser;
            to_service = to_ser;
            this.data = package;
        }
    }
}
