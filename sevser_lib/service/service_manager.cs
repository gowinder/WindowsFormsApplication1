// gowinder@hotmail.com
// gowinder.base_lib
// service_manager.cs
// 2016-05-04-9:34

#region

using System;
using System.Collections.Generic;
using gowinder.base_lib.evnt;

#endregion

namespace gowinder.base_lib.service
{
    public class service_manager : i_service_manager
    {
        private static service_manager _manger;
        protected object _locker;

        private service_manager()
        {
            dict_service = new Dictionary<string, service_base>();
            _locker = new object();
        }

        protected Dictionary<string, service_base> dict_service { get; set; }

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

        public void start_all()
        {
            lock (_locker)
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

        public static i_service_manager instance()
        {
            if (_manger == null)
                _manger = new service_manager();

            return _manger;
        }
    }
}