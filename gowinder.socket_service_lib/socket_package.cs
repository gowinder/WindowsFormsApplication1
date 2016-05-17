// gowinder@hotmail.com
// gowinder.socket_service_lib
// socket_package.cs
// 2016-05-10-14:11

#region

using System;
using System.Text;
using gowinder.base_lib;
using gowinder.base_lib.service;
using gowinder.net_base;

#endregion

namespace gowinder.socket_service_lib
{
    public class socket_package : net_package
    {
        public socket_package(service_base from_service, int s, byte[] buff, int offset, int length)
            : base(from_service)
        {
            size = (uint) s;
            var temp_buffer = new byte[length];
            Array.Copy(buff, offset, temp_buffer, 0, length);

            data = Convert.ToString(temp_buffer);


            is_parsed = false;
        }

        public override byte[] get_transfer_buffer()
        {
            var str_json = data as string;
            if (str_json == null)
                throw new ArgumentException("socket_package.get_transfer_buffer");
            return Encoding.UTF8.GetBytes(str_json);
        }

        public override void parse_data()
        {
            var str_json = data as string;
            if (str_json == null)
                throw new ArgumentException("socket_package.parse_data");
        }
    }
}