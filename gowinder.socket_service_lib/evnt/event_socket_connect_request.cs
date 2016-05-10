// gowinder@hotmail.com
// gowinder.socket_service_lib
// event_socket_connect_request.cs
// 2016-05-10-14:11

#region

using System;
using System.Net.Sockets;
using gowinder.base_lib;
using gowinder.base_lib.evnt;

#endregion

namespace gowinder.socket_service_lib.evnt
{
    public class socket_connect_info
    {
        public string host { get; set; }
        public int port { get; set; }
        public connect_socket_status status { get; set; }
        public SocketError sock_error { get; set; }
        public uint session_id { get; set; }

        public event_base band_event { get; set; }

        /// <summary>
        /// auto reconnect when disconnect or connect failed
        /// </summary>
        public bool auto_reconnect { get; set; }
        /// <summary>
        /// silent when connect result
        /// </summary>
        public bool silent { get; set; }
    }

    public class event_socket_connect_request : event_base
    {
        public const string type = "event_socket_connect_request";

        public event_socket_connect_request() : base(type)
        {
        }

        public socket_connect_info connect_info
        {
            get { return data as socket_connect_info; }
            set { data = value; }
        }

        public void set(service_base from, service_base to, socket_connect_info info)
        {
            from_service = from;
            to_service = to;
            data = info;
            info.band_event = this;
        }

        public override void process()
        {
            var socket_ser = to_service as socket_service;
            if (socket_ser == null)
                throw new Exception("event_socket_connect_request.process to_service is not socket_service");

            socket_ser.connect(this);
        }
    }
}