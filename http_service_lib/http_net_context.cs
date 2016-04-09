using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.http_service_lib
{
    public class http_net_context : net_context
    {
        public HttpListenerContext ctx { get; set; }

        public http_net_context(uint i, long ed) : base(i, ed)
        {
        }
    }
}
