using gowinder.base_lib;
using gowinder.http_service_lib.evnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gowinder.net_base
{
    /// <summary>
    /// net packet form
    /// </summary>
    public enum net_package_carrier
    {
        none = 0,
        http = 1,
        socket = 2,
    }

    public class net_package : ICloneable, i_async_wait_action
    {
        public net_package(service_base from_service)
        {
            is_parsed = false;
        }

        public uint type { get; set; }
        public uint sub_type { get; set; }
        public uint size { get; set; }
        public uint index { get; set; }
        public string token { get; set; }
        public object data { get; set; }
        public net_package_carrier carrier { get; set; }
        public service_base from_service { get; set; }

        public virtual int ret
        {
            get
            {
                if (data is string)
                {
                    JObject jo = (JObject)JsonConvert.DeserializeObject(data as string);
                    return (int)jo[net_json_name.ret];
                }
                return -1;
            }
            set
            {
                if (data is string)
                {
                    JObject jo = (JObject)JsonConvert.DeserializeObject(data as string);
                    jo[net_json_name.ret] = value;
                }
            }
        }

        /// <summary>
        /// 消息是否解析过
        /// </summary>
        public bool is_parsed { get; set; }

        /// <summary>
        /// use to send back package to where it come from the from_service
        /// </summary>
        public void send_back(service_base send_back_from_service)
        {
            var temp_e = from_service.get_new_event(event_send_package.type) as event_send_package;
            temp_e.set(send_back_from_service, from_service, this);
        }

        public object Clone()
        {
            var c = new net_package(from_service);
            return c;
        }


        public virtual void process() { }
        public void resume_process()
        {
            process();
        }
    }
}
