// gowinder@hotmail.com
// gowinder.net_base
// net_package.cs
// 2016-05-10-14:11

#region

using System;
using gowinder.base_lib;
using gowinder.base_lib.service;
using gowinder.net_base.evnt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace gowinder.net_base
{
    /// <summary>
    ///     net packet form
    /// </summary>
    public enum net_package_carrier
    {
        none = 0,
        http = 1,
        socket = 2
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
        public service_base process_service { get; set; }
        public object owner { get; set; }

        public virtual int ret
        {
            get
            {
                if (data is string)
                {
                    var jo = (JObject) JsonConvert.DeserializeObject(data as string);
                    return (int) jo[net_json_name.ret];
                }
                if (data is JObject)
                {
                    var jo = (JObject) data;
                    return (int) jo[net_json_name.ret];
                }
                throw new NotImplementedException("net_package ret get not implemented for none json type");
            }
            set
            {
                if (data is string)
                {
                    var jo = (JObject) JsonConvert.DeserializeObject(data as string);
                    jo[net_json_name.ret] = value;
                }
                else if (data is JObject)
                {
                    var jo = (JObject) data; //
                    jo[net_json_name.ret] = value;
                }
                else
                {
                    throw new NotImplementedException("net_package ret set not implemented for none json type");
                }
            }
        }

        /// <summary>
        ///     消息是否解析过
        /// </summary>
        public bool is_parsed { get; set; }

        public void resume_process()
        {
            process();
        }

        public object Clone()
        {
            var c = new net_package(from_service);
            return c;
        }

        public virtual byte[] get_transfer_buffer()
        {
            throw new NotImplementedException();
        }

        public virtual void parse_data()
        {
        }

        /// <summary>
        ///     use to send back package to where it come from the from_service
        /// </summary>
        public void send_back(service_base send_back_from_service)
        {
            var temp_e = from_service.get_new_event(event_send_package.type) as event_send_package;
            if (temp_e == null)
                throw new Exception("package_action.send_back get new event_send_package failed");
            var recv_info = owner as receive_package_info;
            if (recv_info == null)
                throw new Exception("package_action.send_back owner as receive_package_info is null");
            var send_info = new send_package_info {context_id = recv_info.context_id, package = this};
            owner = send_info;

            temp_e.set(send_back_from_service, from_service, send_info);
            temp_e.send();
        }


        public virtual void process()
        {
        }
    }
}