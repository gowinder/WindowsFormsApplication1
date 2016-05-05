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
            byte[] temp_buffer = new byte[length];
            Array.Copy(buff, offset, temp_buffer, 0, length);

            this.data = Convert.ToString(temp_buffer) as string;



            is_parsed = false;
        }

        public override void parse_data()
        {
            string str_json = this.data as string;
            if(str_json == null)
                throw new ArgumentException("socket_package.parse_data");


        }
    }
}