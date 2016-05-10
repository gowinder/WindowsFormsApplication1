// gowinder@hotmail.com
// gowinder.game_base_lib
// http_string_net_package_parser.cs
// 2016-05-10-14:11

#region

using System;
using WindowsFormsApplication1.net;
using gowinder.base_lib;
using gowinder.net_base;
using Newtonsoft.Json.Linq;

#endregion

namespace WindowsFormsApplication1.data
{
    public class http_string_net_package_parser : i_net_package_parser
    {
        public http_string_net_package_parser(service_base from_ser)
        {
            from_serivce = from_ser;
        }

        public service_base from_serivce { get; set; }

        public net_package parse(object data, int offset, int length)
        {
            var buff = data as string;
            if (buff == null)
                throw new ArgumentException("");
            var json_root = JObject.Parse(buff);
            var package_type = (net_package_type) (int) json_root[net_json_name.package_type];
            net_package package = null;
            if (package_type == net_package_type.action)
                package = new net_package_action(from_serivce, null);
            try
            {
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