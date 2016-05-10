// gowinder@hotmail.com
// gowinder.net_base
// event_receive_package.cs
// 2016-05-10-14:11

#region

using gowinder.base_lib;
using gowinder.base_lib.evnt;

#endregion

namespace gowinder.net_base.evnt
{
    public class receive_package_info
    {
        //       public net_context context { get; set; }
        public uint context_id { get; set; }
        public net_package package { get; set; }
    }

    public class event_receive_package : event_base
    {
        public const string type = "event_receive_package";

        public event_receive_package() : base(type)
        {
        }

        public void set(service_base from, service_base to, receive_package_info info)
        {
            from_service = from;
            to_service = to;
            data = info;
        }
    }
}