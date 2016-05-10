// gowinder@hotmail.com
// gowinder.base_lib
// event_pump.cs
// 2016-05-10-14:11

#region

using System;
using System.Collections.Generic;
using System.Threading;

#endregion

namespace gowinder.base_lib.evnt
{
    public class event_pump : i_event_pump
    {
        protected bool _is_open;
        protected object _locker;
        //protected Queue<event_base> _queue_recycle;
        protected Dictionary<string, Queue<event_base>> _map_recycle;


        protected string _name;
        protected Queue<event_base> _queue;
        protected ManualResetEvent _waiter;


        public event_pump(string name, service_base ser)
        {
            _name = name;
            _waiter = new ManualResetEvent(false);
            _locker = new object();
            _queue = new Queue<event_base>();
            _map_recycle = new Dictionary<string, Queue<event_base>>();
            //event_builder = new base_event_builder();
            service = ser;
        }

        public service_base service { get; protected set; }

        public string id
        {
            get { return _name; }
        }


        public void push(event_base e)
        {
            lock (_locker)
            {
                if (!_is_open)
                    throw new Exception("event pump not open");


                _queue.Enqueue(e);
                _waiter.Set();
            }
        }

        public event_base pop()
        {
            lock (_locker)
            {
                if (!_is_open)
                    throw new Exception("event pump not open");


                if (_queue.Count < 1)
                    return null;
                var e = _queue.Dequeue();
                return e;
            }
        }

        public int size()
        {
            lock (_locker)
            {
                return _queue.Count;
            }
        }

        public bool wait(int mill_second)
        {
            if (size() > 0)
                return true;

            _waiter.Reset();
            return _waiter.WaitOne(mill_second);
        }

        public void close()
        {
            throw new NotImplementedException();
        }

        public void open()
        {
            lock (_locker)
            {
                _is_open = true;
            }
        }

        public bool is_open()
        {
            return _is_open;
        }


        public void recycle(event_base e)
        {
            if (_map_recycle.ContainsKey(e.event_type))
            {
                var queue_recyle = _map_recycle[e.event_type];
                queue_recyle.Enqueue(e);
            }
            else
            {
                var queue_recyle = new Queue<event_base>();
                queue_recyle.Enqueue(e);
                _map_recycle[e.event_type] = queue_recyle;
            }
        }


        public event_base get_new_event(string event_type)
        {
            lock (_locker)
            {
                if (_map_recycle.ContainsKey(event_type))
                {
                    var queue_recyle = _map_recycle[event_type];
                    if (queue_recyle.Count > 0)
                        return queue_recyle.Dequeue();
                }
                else
                {
                    var e = service.event_builder.build_event(event_type);
                    if (e != null)
                        e.owner_pump = this;

                    return e;
                }
            }

            return null;
        }
    }
}