using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using gowinder.net_base;
using gowinder.net_base.evnt;

namespace gowinder.http_service_lib.evnt
{
    [Serializable]
    public class event_http_send_package : event_send_package
    {
        public event_http_send_package()
        {
        }
        public override void process()
        {
            http_service service = to_service as http_service;
            if(service == null)
                throw new Exception("event_http_send_package.process to service is not http_service");

            service.send_response(this.data as send_package_info);
        }
    }
}
