using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib;
using gowinder.base_lib.evnt;
using gowinder.net_base;

namespace gowinder.net_base.evnt
{
    [Serializable]
    public class receive_package_info
    {
        public net_context context { get; set; }
        public net_package package { get; set; }
    }

    [Serializable]
    public class event_receive_package : event_base
    {
        public const string type = "event_receive_package";

        public void set(service_base from, service_base to, receive_package_info info)
        {
            from_service = from;
            to_service = to;
            this.data = info;
        }

        public event_receive_package() : base(type)
        {
            
        }
    }
}
