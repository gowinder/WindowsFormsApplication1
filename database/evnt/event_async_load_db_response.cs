using gowinder.base_lib.evnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.database.evnt
{
    public class async_load_db_response : async_load_db_request
    {
        public Dictionary<string, object> dict_result { get; set; }
    }

    abstract public class event_async_load_db_response : event_base
    {
        public const string type = "event_async_load_db_response";
    }
}
