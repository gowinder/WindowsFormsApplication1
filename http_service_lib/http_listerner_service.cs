using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gowinder.http_service
{
    class http_listerner_service
    {
        class my_context
        {
            public my_context(uint i, long ed) { this.id = i;this.end_time = ed; done = false; }

            public uint id { get; set; }
            public long end_time { get; set; }
            public HttpListenerContext ctx { get; set; }

            public bool done { get; set; }
        }
        Dictionary<uint, my_context> dict_context;

        protected object _lock;

        public http_listerner_service()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://127.0.0.1:9981/test_request/");
            dict_context = new Dictionary<uint, my_context>();

            _lock = new object();
        }

        private uint _current_id = 0;
        protected uint current_id { get { return ++_current_id; }  }

        protected HttpListener _listener;
        public Task run()
            {
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
                        sim_process_context();
                    }
                });
            }


        Dictionary<uint, my_context> _dict_delete = new Dictionary<uint, my_context>();
        private void sim_process_context()
        {
            lock (_lock)
            {
                foreach (var del_id in _dict_delete.Keys)
                {
                    dict_context.Remove(del_id);
                    Console.WriteLine("remove id, thread={0}, task={1}, id:{2}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId, del_id);
                }
                _dict_delete.Clear();
            }

            lock (_lock)
            {
                if (dict_context.Count < 1)
                    return;
            }

            Dictionary<uint, my_context> dict_copy = new Dictionary<uint, my_context>();
            lock(_lock)
            {
                foreach (var mctx in dict_context.Values)
                {
                    dict_copy.Add(mctx.id, mctx);
                }
            }

            Console.WriteLine("sim_process_context , thread={0}, task={1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);

            foreach (var mctx in dict_copy.Values)
            {
                if (mctx.end_time < utility.get_tick())
                    continue;

                if (mctx.done)
                    continue;

                Task.Run(async () =>
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append(mctx.id);
                    sb.Append(" end");
                    string str = sb.ToString();
                    byte[] bb = System.Text.Encoding.UTF8.GetBytes(str);
                    await mctx.ctx.Response.OutputStream.WriteAsync(bb, 0, bb.Length);
                    mctx.ctx.Response.Close();
                    mctx.done = true;
                    Console.WriteLine("response in task, thread={0}, task={1}, id:{2}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId, mctx.id);
                    lock (_lock)
                    {
                        Console.WriteLine("add del response in task, thread={0}, task={1}, id:{2}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId, mctx.id);
                        _dict_delete.Add(mctx.id, mctx);
                    }

                });
                Console.WriteLine("response, thread={0}, task={1}, id:{2}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId, mctx.id);
            }

           
        }

        private void process_request(HttpListenerContext ctx, http_listerner_service http_service)
        {
            Console.WriteLine("process_request, thread={0}, task={1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);

            uint id = Convert.ToUInt32(ctx.Request.QueryString["id"]);
            long end_time = Convert.ToInt32(ctx.Request.QueryString["sleep"]);
            end_time += utility.get_tick();
            var my_context = new my_context(this.current_id, end_time);
            my_context.ctx = ctx;

            lock(_lock)
            {
                dict_context.Add(my_context.id, my_context);
            }
            

        }
    }
          

        
}


