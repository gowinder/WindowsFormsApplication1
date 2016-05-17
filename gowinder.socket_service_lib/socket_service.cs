// gowinder@hotmail.com
// gowinder.socket_service_lib
// socket_service.cs
// 2016-05-10-14:11

#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using gowinder.base_lib;
using gowinder.base_lib.evnt;
using gowinder.base_lib.service;
using gowinder.net_base;
using gowinder.net_base.evnt;
using gowinder.socket_service_lib.evnt;

#endregion

namespace gowinder.socket_service_lib
{
    public class socket_service : service_base, i_net_context_manager
    {
        public static string default_name = "socket_service";

        public enum service_type
        {
            server,
            client
        }

        protected object _lock_context_id;

        protected object _lock_dict_context;
        private uint _socket_id;


        public socket_service(service_type type, string service_name = "")
        {
            if(service_name == "")
                name = default_name;
            else
            {
                name = service_name;
            }
            server_cient_type = type;
            _lock_context_id = new object();
            _lock_dict_context = new object();
            dict_context = new Dictionary<uint, net_context>();
            dict_context_by_session_id = new Dictionary<uint, net_context>();
        }

        public service_type server_cient_type { get; protected set; }

        protected Dictionary<uint, net_context> dict_context { get; set; }
        protected Dictionary<uint, net_context> dict_context_by_session_id { get; set; }
        public i_net_package_parser net_package_parser { get; set; }

        protected uint new_socket_id
        {
            get { return _socket_id++; }
        }

        public void add_context(net_context context)
        {
            lock (_lock_dict_context)
            {
                dict_context.Add(context.id, context);
            }
        }

        public net_context find_by_id(uint id)
        {
            lock (_lock_dict_context)
            {
                if (dict_context.ContainsKey(id))
                    return dict_context[id];

                return null;
            }
        }

        public void remove_by_id(uint id)
        {
            lock (_lock_dict_context)
            {
                dict_context.Remove(id);
            }
        }

        public uint get_new_id()
        {
            lock (_lock_context_id)
            {
                return new_socket_id;
            }
        }

        internal void connect(event_socket_connect_request event_request)
        {
            var info = event_request.connect_info;
            if (server_cient_type != service_type.client)
                throw new Exception("socket_service.connect is not a server type service");

            var context = find_by_session_id(info.session_id);
            if (context == null)
            {
                var socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);
                socket.Blocking = false;
               
               
                context = new socket_net_context(net_package_parser, new_socket_id, socket);
                context.connect(event_request);
            }
            else
            {
                if (context.status != socket_net_context.connect_status.disconnected)
                {
                    context.connect(event_request);
                }
            }
        }
        public socket_net_context find_by_session_id(uint session_id)
        {
            if (!dict_context_by_session_id.ContainsKey(session_id))
                return null;

            return dict_context_by_session_id[session_id] as socket_net_context;
        }

        public void send_package(send_package_info send_package_info)
        {
            var context = find_by_id(send_package_info.context_id) as socket_net_context;
            if (context == null)
                throw new Exception("socket_service send_package socket_net_context not found id=" +
                                    send_package_info.context_id);

            var buffer = send_package_info.package.get_transfer_buffer();
            if (buffer == null)
                throw new Exception("");
            context.send(buffer, 0, buffer.Length);
        }

        protected override i_event_builder on_create_event_builder()
        {
            return new socket_service_event_builder();
        }

        protected override void on_process_start()
        {
        }

        protected override void on_maintain()
        {
        }

        protected override void init()
        {
        }

        protected class socket_service_event_builder : base_event_builder
        {
            public override event_base build_event(string event_type)
            {
                var e = base.build_event(event_type);
                if (e != null)
                    return e;

                switch (event_type)
                {
                    case event_send_package.type:
                    {
                        return new event_socket_send_package();
                    }
                        break;
                    case event_socket_connect_request.type:
                    {
                        return new event_socket_connect_request();
                    }
                        break;
                }

                return null;
            }
        }
    }
}