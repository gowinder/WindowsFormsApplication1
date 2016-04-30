using gowinder.base_lib;
using gowinder.net_base;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.net;

namespace WindowsFormsApplication1.data
{

    public class my_net_package_parser : i_net_package_string_parser
    {
        public service_base from_serivce { get; set; }

        public my_net_package_parser(service_base from_ser)
        {
            from_serivce = from_ser;
        }

        public net_package parse(string buff)
        {
            net_package package = new net_package(from_serivce);
            try
            {
                var json_root = JObject.Parse(buff);
                package.data = json_root;
                package.type = (uint)json_root[net_json_name.package_type];
                package.sub_type = (uint)json_root[net_json_name.package_sub_type];
                package.index = (uint)json_root[net_json_name.index];
                package.token = (string)json_root[net_json_name.token];
            }
            catch(Exception ex)
            {
                return null;
            }
            package.is_parsed = true;

            return package;
        }
    }
}
