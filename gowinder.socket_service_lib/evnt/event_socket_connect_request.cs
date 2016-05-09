using gowinder.base_lib;
using gowinder.base_lib.evnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.socket_service_lib.evnt
{
    public class socket_connect_info
    {
        public string host { get; set; }
        public uint port { get; set; }
        public connect_socket_status status { get; set; }
        public SocketError sock_error { get; set; }
        public uint session_id { get; set; }
    }

    public class event_socket_connect_request : event_base
    {
        public const string type = "event_socket_connect_request";
        public event_socket_connect_request():base(type)
        {

        }

        public void set(service_base from, service_base to, socket_connect_info info)
        {
            from_service = from;
            to_service = to;
            data = info;
        }

        public socket_connect_info connect_info
        {
            get
            {
                return data as socket_connect_info;
            }
            set
            {
                data = value;
            }
        }

        public override void process()
        {
            socket_service socket_ser = to_service as socket_service;
            if (socket_ser == null)
                throw new Exception("event_socket_connect_request.process to_service is not socket_service");

            socket_ser.connect(this);
        }
    }
}
