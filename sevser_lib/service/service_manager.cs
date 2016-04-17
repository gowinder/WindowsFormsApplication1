using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using gowinder.base_lib.evnt;

namespace gowinder.base_lib.service
{
    public class service_manager : i_service_manager
    {
        protected object _locker;
        protected Dictionary<string, service_base> dict_service { get; set; }
        private service_manager()
        {
            dict_service = new Dictionary<string, service_base>();
            _locker = new Object();
        }
        public void add_service(service_base ser)
        {
            lock (_locker)
            {
                dict_service.Add(ser.name, ser);
            }
        }

        public service_base get_serivce(string name)
        {
            lock (_locker)
            {
                return dict_service[name];
            }

        }

        private static service_manager _manger;
        public static i_service_manager instance()
        {
            if (_manger == null)
                _manger = new service_manager();

            return _manger;
        }

        public void start_all()
        {
            lock(_locker)
            {
                foreach (var ser in dict_service.Values)
                {
                    ser.start_service();
                }
            }
        }

        public void stop_all()
        {
            lock (_locker)
            { 
                foreach (var ser in dict_service.Values)
                {
                    ser.stop_service();
                }
            }
        }

        public void send_all(event_base e)
        {
            throw new NotImplementedException();
            //             MemoryStream ms = e.get_memory_stream();
            //             BinaryFormatter bf = new BinaryFormatter();
            //             foreach (var ser in dict_service.Values)
            //             {
            //                
            //             }
        }
    }
}
