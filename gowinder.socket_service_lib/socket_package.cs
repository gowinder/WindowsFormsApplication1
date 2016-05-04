// gowinder@hotmail.com
// gowinder.socket_service_lib
// net_msg.cs
// 2016-05-04-12:01

using System;
using System.Runtime.InteropServices;
using gowinder.base_lib;
using gowinder.net_base;

namespace gowinder.socket_service_lib
{

    public class socket_package : net_package
    {
        public socket_package(service_base from_service, int s, byte[] buff, int offset, int length):base(from_service)
        {
            size = (uint)s;
            data = new byte[length];
            byte[] bufffer = data as byte[];
            Array.Copy(buff, offset, bufffer, 0, length);

            is_parsed = false;
        }
    }
}