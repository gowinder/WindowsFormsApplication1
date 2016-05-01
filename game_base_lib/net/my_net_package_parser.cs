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
            var json_root = JObject.Parse(buff);
            net_package_type package_type = (net_package_type)(int)json_root[net_json_name.package_type];
            net_package package = null;
            if(package_type == net_package_type.action)
                package = new net_package_action(from_serivce, null);
            try
            {
                package.data = json_root;
                package.type = 
                package.sub_type = (uint)json_root[net_json_name.package_sub_type];
                package.index = (uint)json_root[net_json_name.index];
                package.token = (string)json_root[net_json_name.token];
            }
            catch(Exception ex)
            {
                throw new Exception("net_package.parse invalid parameters");
            }
            package.is_parsed = true;

            return package;
        }
    }
}
