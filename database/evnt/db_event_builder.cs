using gowinder.base_lib.evnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database.evnt
{
    class db_event_builder : base_event_builder
    {
        public event_base build_event(String event_type)
        {
            event_base e = base.build_event(event_type);
            if (e != null)
                return e;

            switch (event_type)
            {
                case event_async_save_db.type:
                    {
                        return new event_async_save_db();
                    }
                default:
                    return null;
            }
        }
    }
}
