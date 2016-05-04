using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib;
using gowinder.net_base;

namespace gowinder.socket_service_lib
{
    public class default_socket_net_package_parser : i_net_package_parser
    {
        public service_base from_service { get; set; }
        public service_base to_service { get; set; }
        public default_socket_net_package_parser(service_base from_ser, service_base to_ser)
        {
            from_service = from_ser;
            to_service = to_ser;
        }
        public net_package parse(object data, int offset, int length)
        {
            byte[] buffer = data as byte[];
            if(buffer == null)
                throw new ArgumentException("default_socket_net_package_parser.parse data is not byte[]");
            socket_package new_package = new socket_package(from_service, length + 4, buffer, offset, length);

            return new_package;
        }
    }
}
