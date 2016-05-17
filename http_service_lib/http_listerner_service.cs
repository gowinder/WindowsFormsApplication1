// gowinder@hotmail.com
// gowinder.http_service_lib
// http_listerner_service.cs
// 2016-05-10-14:11

#region

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using gowinder.base_lib;
using gowinder.base_lib.service;
using gowinder.http_service_lib;
using gowinder.net_base;
using gowinder.net_base.evnt;

#endregion

namespace gowinder.http_service
{
    public class http_listerner_service : service_base
    {
        public static string default_name = "http_listerner_service";
        private uint _current_id;

        protected HttpListener _listener;

        public http_listerner_service(string host_address, string service_name = "")
        {
            if (service_name == "")
                name = default_name;
            else
            {
                name = service_name;
            }
            _listener = new HttpListener();
            _listener.Prefixes.Add(host_address);
        }

        public i_net_context_manager context_manager { get; set; }

        public i_net_package_parser net_package_parser { get; set; }

        public service_base receive_package_service { get; set; }
        public service_base http_ser { get; set; }

        protected uint current_id
        {
            get { return ++_current_id; }
        }

        protected override void on_process_start()
        {
            if (net_package_parser == null)
                throw new Exception("http_listerner_service has not assigned i_net_package_parser");

            if (receive_package_service == null)
                throw new Exception("http_listerner_service has not assigned receive_package_service");
            Task.Run(run);
        }

        public Task run()
        {
            init();
            return Task.Run(() =>
            {
                try
                {
                    _listener.Start();
                }
                catch (HttpListenerException hlex)
                {
                    return;
                }
                Console.WriteLine("run-> begin while, thread={0}, task={1}", Thread.CurrentThread.ManagedThreadId,
                    Task.CurrentId);
                var sem = new Semaphore(5, 5);
                while (true)
                {
                    if (sem.WaitOne(50))
                    {
                        _listener.GetContextAsync().ContinueWith(async t =>
                        {
                            sem.Release();
                            Console.WriteLine("run-> while, thread={0}, task={1}", Thread.CurrentThread.ManagedThreadId,
                                Task.CurrentId);
                            var ctx = await t;
                            process_request(ctx, this);
                        });
                    }
                    //    sim_process_context();
                }
            });
        }

        private void init()
        {
            // todo do the init operation
        }

        protected virtual void process_request(HttpListenerContext ctx, http_listerner_service http_service)
        {
            Console.WriteLine("process_request, thread={0}, task={1}", Thread.CurrentThread.ManagedThreadId,
                Task.CurrentId);

            var my_context = new http_net_context(context_manager.get_new_id());
            my_context.ctx = ctx;


            string input_text;
            using (var reader = new StreamReader(ctx.Request.InputStream,
                ctx.Request.ContentEncoding))
            {
                input_text = reader.ReadToEnd();
            }

            if (input_text != "")
            {
                var p = net_package_parser.parse(input_text, 0, input_text.Length);
                p.from_service = http_ser;
                p.carrier = net_package_carrier.http;
                var e = receive_package_service.get_new_event(event_receive_package.type) as event_receive_package;
                var info = new receive_package_info {context_id = my_context.id, package = p};
                p.owner = info;
                e.set(http_ser, receive_package_service, info);
                e.send();

                context_manager.add_context(my_context);
            }
        }
    }
}