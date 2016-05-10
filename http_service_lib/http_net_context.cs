// gowinder@hotmail.com
// gowinder.http_service_lib
// http_net_context.cs
// 2016-05-10-14:11

#region

using System.Net;
using gowinder.net_base;

#endregion

namespace gowinder.http_service_lib
{
    public class http_net_context : net_context
    {
        public http_net_context(uint i) : base(i)
        {
        }

        public HttpListenerContext ctx { get; set; }
    }
}