// gowinder@hotmail.com
// gowinder.socket_service_lib
// socket_listerner_service.cs
// 2016-05-10-14:11

#region

using System.Net;
using System.Net.Sockets;
using gowinder.base_lib;
using gowinder.net_base;

#endregion

namespace gowinder.socket_service_lib
{
    public class socket_listerner_service : service_base
    {
        public static string default_name = "socket_listerner_service";
        public socket_listerner_service(i_net_context_manager context_manager, string service_name = "")
        {
            if (service_name == "")
                name = default_name;
            else
            {
                name = service_name;
            }
            net_context_manager = context_manager;
        }

        /// <summary>
        ///     the service that handle recv package event, default point to a logic_serive, or you can point to some proxy service
        ///     to dispatch package to other service
        /// </summary>
        public service_base receive_package_service { get; set; }

        /// <summary>
        ///     the service that handle send package event, default point to a http_service
        /// </summary>
        public service_base send_package_service { get; set; }

        protected Socket listerner_socket { get; set; }
        public i_net_context_manager net_context_manager { get; set; }
        public i_net_package_parser net_package_parser { get; set; }


        protected override void on_process_start()
        {
        }

        protected override void on_maintain()
        {
            var socket = listerner_socket.Accept();

            var context = new socket_net_context(net_package_parser, net_context_manager.get_new_id(), socket)
            {
                send_package_service = send_package_service,
                receive_package_service = receive_package_service
            };

            context.receive_package_service = receive_package_service;
            net_context_manager.add_context(context);
        }

        protected override void init()
        {
            listerner_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listerner_socket.Bind(new IPEndPoint(IPAddress.Any, 928));
            listerner_socket.Listen(0);
        }
    }
}