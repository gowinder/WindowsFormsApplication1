using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.service;
using gowinder.http_service_lib.evnt;
using gowinder.net_base.evnt;

namespace WindowsFormsApplication1.evnt
{
    class event_my_receive_package : event_receive_package
    {
        public override void process()
        {
            my_logic_service ser = to_service as my_logic_service;
            if(ser == null)
                throw new Exception("event_my_receive_package.process to_service is not my_logic_service");

            ser.receive_package(this);
        }
    }
}
