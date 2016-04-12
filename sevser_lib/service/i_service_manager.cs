using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib.evnt;

namespace gowinder.base_lib.service
{
    public interface i_service_manager
    {
        void add_service(service_base ser);
        service_base get_serivce(string name);
        void start_all();
        void stop_all();
        void send_all(event_base e);
    }
}
