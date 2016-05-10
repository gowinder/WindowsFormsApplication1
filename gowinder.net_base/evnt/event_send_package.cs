// gowinder@hotmail.com
// gowinder.net_base
// event_send_package.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.base_lib;
using gowinder.base_lib.evnt;

#endregion

namespace gowinder.net_base.evnt
{
    [Serializable]
    public class send_package_info
    {
        //  public net_context context { get; set; }
        public uint context_id { get; set; }
        public net_package package { get; set; }
    }

    [Serializable]
    public class event_send_package : event_base
    {
        public const string type = "event_send_package";

        public event_send_package() : base(type)
        {
        }

        public void set(service_base from_ser, service_base to_ser, send_package_info package_info)
        {
            from_service = from_ser;
            to_service = to_ser;
            data = package_info;
        }
    }
}