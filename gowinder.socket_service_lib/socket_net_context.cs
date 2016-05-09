// gowinder@hotmail.com
// gowinder.socket_service_lib
// socket_net_context.cs
// 2016-05-04-9:35

#region

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using gowinder.base_lib;
using gowinder.net_base;
using gowinder.net_base.evnt;

#endregion

namespace gowinder.socket_service_lib
{
    public class socket_net_context : net_context
    {
        public socket_net_context(i_net_package_parser net_package_parser, uint i, Socket band_sock, uint recv_buffer_size = 1024*10) : base(i)
        {
            this.net_package_parser = net_package_parser;
            this.recv_buffer_size = recv_buffer_size;
            recv_buffer = new byte[recv_buffer_size];
            recv_buffer_offset = 0;

            queue_send_async_event_args_activate = new Queue<SocketAsyncEventArgs>();
            list_send_async_event_args_free = new List<SocketAsyncEventArgs>();

            sock = band_sock;
            recv_async_args = new SocketAsyncEventArgs();
            recv_async_args.Completed += Args_Completed;
            recv_async_args.UserToken = sock;
            sock.ReceiveAsync(recv_async_args);
        }

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
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                {
                    on_send(e);
                }
                    break;

                case SocketAsyncOperation.Receive:
                {
                    on_recv(e);
                }
                    break;

                case SocketAsyncOperation.Send:

                    break;

                default:
                    throw new Exception("Invalid operation completed");
            }
        }

        private void on_send(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                reset_args(e);
                list_send_async_event_args_free.Add(e);
            }
            else
            {
                var sock = e.UserToken as Socket;
                sock.Shutdown(SocketShutdown.Receive);
                sock.Close();
            }
        }

        protected void on_recv(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                if(net_package_parser == null)
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
                    int package_length = Marshal.ReadInt32(recv_buffer, 0);
                    while (package_length > recv_buffer_offset)
                    {
                        socket_package p = net_package_parser.parse(recv_buffer, recv_buffer_offset + 4, package_length - 4) as socket_package;
                        
                        var evnt = receive_package_service.get_new_event(event_receive_package.type) as event_receive_package;

                        var info = new receive_package_info { context_id = this.id, package = p };
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
                var sock = e.UserToken as Socket;
                sock.Shutdown(SocketShutdown.Receive);
                sock.Close();
            }
        }

        public void send(byte[] send_data, int offset, int length)
        {
            SocketAsyncEventArgs args = get_send_args();

            args.UserToken = sock;
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
                int last_one_index = list_send_async_event_args_free.Count - 1;
                SocketAsyncEventArgs args = list_send_async_event_args_free[last_one_index];
                
                list_send_async_event_args_free.RemoveAt(last_one_index);
                return args;
            }
            else
            {
                if (queue_send_async_event_args_activate.Count > max_send_async_event_args_size)
                {
                    throw new Exception("socket_net_context.get_send_args activate send async event args overflowed");
                }

                return new SocketAsyncEventArgs();
            }
        }
    }
}