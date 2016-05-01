using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using gowinder.base_lib;
using gowinder.server_lib;
using gowinder.net_base;
using System.IO;
using gowinder.base_lib.service;
using gowinder.http_service_lib;
using gowinder.http_service_lib.evnt;
using ProtoBuf;

namespace gowinder.http_service
{
    public class http_listerner_service : service_base
    {
        public i_net_context_manager context_manager { get; set; }

        public i_net_package_string_parser net_package_parser { get; set; }
       
        public service_base receive_package_service { get; set; }

        public http_listerner_service()
        {
            name = "http_listerner_service";
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://127.0.0.1:9981/test_request/");
            
        }

        private uint _current_id = 0;
        protected uint current_id { get { return ++_current_id; }  }

        protected override void on_process_start()
        {
            if(net_package_parser == null)
                throw new Exception("http_listerner_service has not assigned i_net_package_parser");

            if(receive_package_service == null)
                throw new Exception("http_listerner_service has not assigned receive_package_service");
            Task.Run(run);
        }

        protected HttpListener _listener;
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
                    Console.WriteLine("run-> begin while, thread={0}, task={1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                    var sem = new Semaphore(5, 5);
                    while (true)
                    {
                        if (sem.WaitOne(50))
                        {
                            _listener.GetContextAsync().ContinueWith(async (t) =>
                            {
                                sem.Release();
                                Console.WriteLine("run-> while, thread={0}, task={1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
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

            Stream s = ctx.Request.InputStream;
            uint id = Convert.ToUInt32(ctx.Request.QueryString["id"]);
            long end_time = Convert.ToInt32(ctx.Request.QueryString["sleep"]);
            end_time += utility.get_tick();
            var my_context = new http_net_context(this.current_id, end_time);
            my_context.ctx = ctx;


            string input_text;
            using (var reader = new StreamReader(ctx.Request.InputStream,
                                     ctx.Request.ContentEncoding))
            {
                input_text = reader.ReadToEnd();
            }

            if (input_text != "")
            {
                System.Text.Encoding encoding = ctx.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(s, encoding);

                net_package p = net_package_parser.parse(reader.ReadToEnd());

                event_receive_package e = receive_package_service.get_new_event(event_http_receive_package.type) as event_receive_package;
                receive_package_info info = new receive_package_info() {context = my_context,package = p};
                e.set(this, receive_package_service, info);
                e.send();

                context_manager.add_context(my_context);
            }
            else
            {
                
            }


            
        }
    }
          

        
}


