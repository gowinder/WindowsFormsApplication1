// gowinder@hotmail.com
// gowinder.http_service_lib
// http_service.cs
// 2016-05-04-9:34

#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using gowinder.base_lib;
using gowinder.base_lib.evnt;
using gowinder.http_service_lib.evnt;
using gowinder.net_base;
using gowinder.net_base.evnt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace gowinder.http_service_lib
{
    public class http_service : service_base, i_net_context_manager
    {
        public static string default_name = "http_service";

        private readonly Dictionary<uint, net_context> dict_context;

        private uint _context_id;
        protected object _lock_contect_id;

        protected object _lock_dict_context;

        public http_service()
        {
            name = default_name;
            dict_context = new Dictionary<uint, net_context>();
            _lock_dict_context = new object();
            start_own_thread = true;
            _lock_contect_id = new object();
        }

        protected uint new_context_id
        {
            get { return _context_id++; }
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
            lock (_lock_contect_id)
            {
                return new_context_id;
            }
        }

        protected override void on_maintain()
        {
        }

        protected override i_event_builder on_create_event_builder()
        {
            return new http_service_event_builder();
            ;
        }

        public void send_response(send_package_info info)
        {
            if (info == null)
                throw new Exception("http_service.send_response send_package_info is null");
            if (info.context == null)
                throw new Exception("http_service.send_response send_package_info.context is null");
            var mctx = info.context as http_net_context;
            if (mctx == null)
                throw new Exception("http_service.send_response info.context is not http_net_context");
            Task.Run(async () =>
            {
                var str = "";
                if (info.package.data is JObject)
                    str = JsonConvert.SerializeObject(info.package.data);
                else if (info.package.data is string)
                    str = (string) info.package.data;
                else
                {
                    throw new NotImplementedException("http_service.send_response info.package.data not support type");
                }
                var bb = Encoding.UTF8.GetBytes(str);
                await mctx.ctx.Response.OutputStream.WriteAsync(bb, 0, bb.Length);
                mctx.ctx.Response.Close();
                mctx.done = true;
                Console.WriteLine("response in task, thread={0}, task={1}, id:{2}", Thread.CurrentThread.ManagedThreadId,
                    Task.CurrentId, mctx.id);
            });

            remove_by_id(mctx.id);
        }

        protected class http_service_event_builder : base_event_builder
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
                        return new event_http_send_package();
                    }
                        break;
                }

                return null;
            }
        }
    }
}