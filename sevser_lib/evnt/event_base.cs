// gowinder@hotmail.com
// gowinder.base_lib
// event_base.cs
// 2016-05-10-14:11

#region

using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using gowinder.base_lib.service;

#endregion

namespace gowinder.base_lib.evnt
{
    #region

    using event_type = String;

    #endregion

    public enum event_data_type
    {
        byte_array,
        char_array,
        string_obj,
        json,
        netmsg,
        object_class
    }

    [Serializable]
    public abstract class event_base : ICloneable
    {
        public event_base(string type)
        {
            event_type = type;
        }

        public service_base from_service { get; set; }
        public service_base to_service { get; set; }
        public string event_type { get; set; }
        public object data { get; set; }
        public event_data_type data_type { get; set; }
        public ArrayList parameter_list { get; set; }
        public i_event_pump owner_pump { get; set; }

        public object Clone()
        {
            var ms = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Seek(0, SeekOrigin.Begin);
            return bf.Deserialize(ms);
        }

        public virtual void set(service_base from, service_base to, string type, byte[] buff, ArrayList parameters)
        {
            from_service = from;
            to_service = to;
            event_type = type;
            data = buff;
            parameter_list = parameters;
            data_type = event_data_type.byte_array;
        }

        public virtual void set(service_base from, service_base to, string type, object obj, ArrayList parameters)
        {
            from_service = from;
            to_service = to;
            event_type = type;
            data = obj;
            parameter_list = parameters;
            data_type = event_data_type.object_class;
        }

        public virtual void send()
        {
//             if (from_service == null)
//                 throw new Excep(exception_base.RETURN_NULL_REF);

            if (to_service == null)
                throw new Exception("event_base.send() to_service is null");

            if (owner_pump == null)
                throw new Exception("event_base.send() owner_pump is null");

            owner_pump.push(this);
        }

        public void recycle()
        {
            if (owner_pump != null)
            {
                owner_pump.recycle(this);
            }
        }

        public virtual void process()
        {
        }

        public MemoryStream get_memory_stream()
        {
            var ms = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            return ms;
        }
    }
}