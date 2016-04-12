using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib.evnt;
using gowinder.http_service_lib.evnt;

namespace gowinder.http_service_lib
{
    class http_service_event_builder : base_event_builder
    {
        
        public virtual event_base build_event(String event_type)
        {
            event_base e = base.build_event(event_type);
            if (e != null)
                return e;

            switch (event_type)
            {
                case event_send_package.type:
                {
                    return new event_http_send_package();
                }
                    break;

            }

            return null;
        }
    }
}
