// gowinder@hotmail.com
// gowinder.game_base_lib
// socket_string_net_package_parser.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.base_lib;
using gowinder.base_lib.service;
using gowinder.net_base;
using Newtonsoft.Json.Linq;

#endregion

namespace gowinder.socket_service_lib
{
    public class socket_string_net_package_parser : i_net_package_parser
    {
        public socket_string_net_package_parser(service_base from_ser, service_base to_ser)
        {
            from_service = from_ser;
            to_service = to_ser;
        }

        public service_base from_service { get; set; }
        public service_base to_service { get; set; }

        public net_package parse(object data, int offset, int length)
        {
            var buffer = data as byte[];
            if (buffer == null)
                throw new ArgumentException("default_socket_net_package_parser.parse data is not byte[]");
            socket_package package = new socket_package(from_service, length + 4, buffer, offset, length);

            try
            {
                var buff = Convert.ToString(buffer);
                var json_root = JObject.Parse(buff);

                package.data = json_root;
                package.type =
                    package.sub_type = (uint) json_root[net_json_name.package_sub_type];
                package.index = (uint) json_root[net_json_name.index];
                package.token = (string) json_root[net_json_name.token];
            }
            catch (Exception ex)
            {
                throw new Exception("net_package.parse invalid parameters");
            }
            package.is_parsed = true;

            return package;
        }
    }
}