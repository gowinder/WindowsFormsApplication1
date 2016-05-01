using gowinder.base_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using gowinder.base_lib.evnt;
using gowinder.http_service_lib.evnt;

namespace gowinder.http_service_lib
{
    public class http_service : service_base, i_net_context_manager
    {
        Dictionary<uint, net_context> dict_context;

        public static string default_name = "http_service";

        protected object _lock_dict_context;

        public http_service()
        {
            name = default_name;
            dict_context = new Dictionary<uint, net_context>();
            _lock_dict_context = new object();
        }

        protected override void on_maintain()
        {
            
        }

        protected override i_event_builder on_create_event_builder()
        {
            return new http_service_event_builder() as i_event_builder;;
        }

        public void send_response(send_package_info info)
        {
            http_net_context mctx = info.context as http_net_context;
            if(mctx == null)
                throw new Exception("http_service.send_response info.context is not http_net_context");
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
            });

            remove_by_id(mctx.id);
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
                return dict_context[id];
            }
        }

        public void remove_by_id(uint id)
        {
            lock (_lock_dict_context)
            {
                dict_context.Remove(id);
            }
        }
    }
}
