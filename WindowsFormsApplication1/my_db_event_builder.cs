using gowinder.base_lib.evnt;
using gowinder.database.evnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class my_db_event_builder : db_event_builder
    {
        public event_base build_event(String event_type)
        {
            event_base e = base.build_event(event_type);
            if (e != null)
                return e;

            switch (event_type)
            {
                case event_async_load_db_request.type:
                    {
                        return new ();
                    }
                default:
                    return null;
            }
        }
    }
}
}
