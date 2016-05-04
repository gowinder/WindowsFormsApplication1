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
        protected object _lock_context_id;

        protected object _lock_dict_context;
        private uint _socket_id;

        public socket_service()
        {
            _lock_context_id = new object();
            _lock_dict_context = new object();
            dict_context = new Dictionary<uint, net_context>();
        }

        protected Dictionary<uint, net_context> dict_context { get; set; }

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

        public void send_package(send_package_info send_package_info)
        {
            throw new NotImplementedException();
        }

        protected override i_event_builder on_create_event_builder()
        {
            return new socket_service_event_builder();
        }

        protected override void on_process_start()
        {
            throw new NotImplementedException();
        }

        protected override void on_maintain()
        {
            throw new NotImplementedException();
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
                }

                return null;
            }
        }
    }
}