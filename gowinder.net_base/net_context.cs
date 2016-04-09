using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.http_service_lib
{
    public class net_context
    {
        public net_context(uint i, long ed) { this.id = i; this.end_time = ed; done = false; }

        public uint id { get; set; }
        public long end_time { get; set; }

        public bool done { get; set; }
    }
}
