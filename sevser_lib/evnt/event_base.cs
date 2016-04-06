using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gowinder.base_lib.evnt
{
    using System.Collections;
    using event_type = System.String;

    public enum event_data_type
    {
        byte_array,
        char_array,
        string_obj,
        json_data,
        netmsg,
        object_class,
    }

    public class event_base
    {
        public service_base from_service { get; set; }
        public service_base to_service { get; set; }
        public event_type event_type { get; set; }
        public System.Object data { get; set; }
        public event_data_type data_type { get; set; }
        public ArrayList parameter_list { get; set; }
        public i_event_pump owner_pump { get; set; }

        public virtual void set(service_base from, service_base to, event_type type, byte[] buff, ArrayList parameters)
        {
            from_service = from;
            to_service = to;
            event_type = type;
            data = (System.Object)buff;
            parameter_list = parameters;
            data_type = event_data_type.byte_array;
        }

        public virtual void set(service_base from, service_base to, event_type type, Object obj, ArrayList parameters)
        {
            from_service = from;
            to_service = to;
            event_type = type;
            data = (System.Object)obj;
            parameter_list = parameters;
            data_type = event_data_type.object_class;
        }

        public virtual void send()
        {
            //             if (from_service == null)
            //                 throw new exception_base(exception_base.RETURN_NULL_REF);
            // 
            //             if (to_service == null)
            //                 throw new exception_base(exception_base.RETURN_NULL_REF);            
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
    }
}
