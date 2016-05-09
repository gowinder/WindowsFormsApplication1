// gowinder@hotmail.com
// gowinder.socket_service_lib
// socket_service.cs
// 2016-05-04-9:35

#region

using System;
using System.Collections.Generic;
using gowinder.base_lib;
using gowinder.base_lib.evnt;
using gowinder.net_base;
using gowinder.net_base.evnt;
using gowinder.socket_service_lib.evnt;

#endregion

namespace gowinder.socket_service_lib
{
    public class socket_service : service_base, i_net_context_manager
    {
        public enum service_type
        {
            server,
            client,
        }
        protected object _lock_context_id;

        protected object _lock_dict_context;
        private uint _socket_id;
        public service_type server_cient_type { get; protected set; }


        public socket_service(service_type type)
        {
            server_cient_type = type;
            _lock_context_id = new object();
            _lock_dict_context = new object();
            dict_context = new Dictionary<uint, net_context>();
            dict_context_by_session_id = new Dictionary<uint, net_context>();
        }

        protected Dictionary<uint, net_context> dict_context { get; set; }
        protected Dictionary<uint, net_context> dict_context_by_session_id { get; set; }

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

        internal void connect(event_socket_connect_request event_request)
        {
            socket_connect_info info = event_request.connect_info;
            if (server_cient_type != service_type.client)
                throw new Exception("socket_service.connect is not a server type service");

            socket_net_context context = find_by_session_id(info.session_id);
            if(context != null)
            {
                
            }
        }

        public socket_net_context find_by_session_id(uint session_id)
        {
            if (!dict_context_by_session_id.ContainsKey(session_id))
                return null;

            return dict_context_by_session_id[session_id] as socket_net_context;
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

        public void send_package(send_package_info send_package_info)
        {
            socket_net_context context = find_by_id(send_package_info.context_id) as socket_net_context;
            if(context == null)
                throw new Exception("socket_service send_package socket_net_context not found id=" + send_package_info.context_id);

            byte[] buffer = send_package_info.package.get_transfer_buffer();
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