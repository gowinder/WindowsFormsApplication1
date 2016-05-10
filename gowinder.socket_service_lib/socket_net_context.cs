// gowinder@hotmail.com
// gowinder.socket_service_lib
// socket_net_context.cs
// 2016-05-10-14:11

#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using gowinder.base_lib;
using gowinder.base_lib.evnt;
using gowinder.net_base;
using gowinder.net_base.evnt;
using gowinder.socket_service_lib.evnt;

#endregion

namespace gowinder.socket_service_lib
{
    public class socket_net_context : net_context
    {
        public enum connect_status
        {
            conntected,
            connecting,
            disconnected,
        }
        public socket_net_context(i_net_package_parser net_package_parser, uint i, Socket band_sock,
            uint recv_buffer_size = 1024*10) : base(i)
        {
            status = connect_status.disconnected;
            this.net_package_parser = net_package_parser;
            this.recv_buffer_size = recv_buffer_size;
            recv_buffer = new byte[recv_buffer_size];
            recv_buffer_offset = 0;

            queue_send_async_event_args_activate = new Queue<SocketAsyncEventArgs>();
            list_send_async_event_args_free = new List<SocketAsyncEventArgs>();

            sock = band_sock;
            recv_async_args = new SocketAsyncEventArgs();
            recv_async_args.Completed += Args_Completed;
            recv_async_args.UserToken = this;
            sock.ReceiveAsync(recv_async_args);
        }

        protected socket_connect_info connect_info { get; set; }

        public connect_status status { get; set; }
        public Socket sock { get; protected set; }
        protected Queue<SocketAsyncEventArgs> queue_send_async_event_args_activate { get; set; }
        protected List<SocketAsyncEventArgs> list_send_async_event_args_free { get; set; }
        protected SocketAsyncEventArgs recv_async_args { get; set; }
        protected byte[] recv_buffer { get; set; }
        protected uint recv_buffer_size { get; set; }
        public uint max_send_async_event_args_size { get; set; }
        protected int recv_buffer_offset { get; set; }
        public service_base receive_package_service { get; set; }
        public service_base send_package_service { get; set; }
        public i_net_package_parser net_package_parser { get; set; }

        private void Args_Completed(object sender, SocketAsyncEventArgs e)
        {
            var context = e.UserToken as socket_net_context;
            if(context == null)
                throw new Exception("socket_net_context.Args_completed userToken is not socket_net_context");
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                {
                        context.on_connect(e);
                }
                    break;

                case SocketAsyncOperation.Receive:
                {
                        context.on_recv(e);
                }
                    break;

                case SocketAsyncOperation.Send:
                {
                        context.on_send(e);
                    }
                    break;
                default:
                    throw new Exception("Invalid operation completed");
            }
        }

        private void on_connect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                connect_info.status = connect_socket_status.connected;
                status = connect_status.conntected;
            }
            else
            {
                connect_info.status = connect_socket_status.failed;
                connect_info.sock_error = e.SocketError;

                if (connect_info.auto_reconnect)
                {
                    connect(connect_info.band_event as event_socket_connect_request);
                }
                else
                    close(SocketShutdown.Both);
            }

            if (!connect_info.silent)
            {
                event_base e_request = connect_info.band_event;
                event_socket_connect_response event_response = e_request.from_service.get_new_event(event_socket_connect_response.type) as event_socket_connect_response;
                if(event_response == null)
                    throw new NullReferenceException("socket_net_context.on_connect event_response");
                event_response.set(e_request.from_service, e_request.to_service, connect_info);
                event_response.send();
            }
        }

        private void on_send(SocketAsyncEventArgs e)
        {
            SocketAsyncEventArgs e_in_queue = queue_send_async_event_args_activate.Dequeue();
            if(e_in_queue != e)
                throw new Exception("socket_net_context.on_send e not equal to queue_send_async_event_args_activate.Dequeue");

            if (e.SocketError == SocketError.Success)
            {
                reset_args(e);
                list_send_async_event_args_free.Add(e);
            }
            else
            {
                close(SocketShutdown.Receive);
            }

            list_send_async_event_args_free.Add(e);
            
        }

        private void close(SocketShutdown reason)
        {
            sock.Shutdown(SocketShutdown.Receive);
            sock.Close();
        }

        protected void on_recv(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                if (net_package_parser == null)
                    throw new Exception("socket_net_context.on_recv net_package_parser is null");

                if (receive_package_service == null)
                    throw new Exception("socket_net_context.on_recv receive_package_service is null");

                recv_buffer_offset += e.BytesTransferred;

                // recv data 
                if (recv_buffer_offset > 0 && e.BytesTransferred > 0)
                {
                    //  TODO LIST encrypt date
                    //

                    //  check package all receive
                    var package_length = Marshal.ReadInt32(recv_buffer, 0);
                    while (package_length > recv_buffer_offset)
                    {
                        var p =
                            net_package_parser.parse(recv_buffer, recv_buffer_offset + 4, package_length - 4) as
                                socket_package;

                        var evnt =
                            receive_package_service.get_new_event(event_receive_package.type) as event_receive_package;

                        var info = new receive_package_info {context_id = id, package = p};
                        p.owner = info;
                        evnt.set(send_package_service, receive_package_service, info);
                        evnt.send();
                    }
                }


                if (!sock.ReceiveAsync(e))
                {
                    Args_Completed(this, e);
                }
            }
            else
            {
                close(SocketShutdown.Receive);
            }
        }

        public void send(byte[] send_data, int offset, int length)
        {
            var args = get_send_args();

            args.UserToken = this;
            args.Completed += Args_Completed;
            args.SetBuffer(send_data, offset, length);

            if (!sock.SendAsync(args))
            {
                Args_Completed(this, args);
            }
        }

        private void reset_args(SocketAsyncEventArgs args)
        {
            args.Completed -= Args_Completed;
            args.SetBuffer(null, 0, 0);
            args.UserToken = null;
        }

        private SocketAsyncEventArgs get_send_args()
        {
            if (list_send_async_event_args_free.Count > 0)
            {
                var last_one_index = list_send_async_event_args_free.Count - 1;
                var args = list_send_async_event_args_free[last_one_index];

                list_send_async_event_args_free.RemoveAt(last_one_index);
                return args;
            }
            if (queue_send_async_event_args_activate.Count > max_send_async_event_args_size)
            {
                throw new Exception("socket_net_context.get_send_args activate send async event args overflowed");
            }

            return new SocketAsyncEventArgs();
        }

        public void connect(event_socket_connect_request event_request)
        {
            if(status == connect_status.connecting)
                throw new Exception("socket_net_context.connect already connecting");

            if(status == connect_status.conntected)
                throw new Exception("socket_net_context.connect already connected");
            if(sock.Connected)
                throw new Exception("socket_net_context.connect already connected");

            var info = event_request.connect_info;
            IPAddress ip = IPAddress.Parse(info.host);
            EndPoint ep = new IPEndPoint(ip, info.port);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed += Args_Completed;
            args.UserToken = this;
            args.RemoteEndPoint = ep;

            connect_info = event_request.connect_info;
            sock.ConnectAsync(args);
            status = connect_status.connecting;
        }
    }
}